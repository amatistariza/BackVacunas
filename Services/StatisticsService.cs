using System;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.IServices;
using API.DTO;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace API.Services;

public class StatisticsService : IStatisticsService
{
    private readonly AplicationDbContext _db;
    private readonly IMemoryCache _cache;

    public StatisticsService(AplicationDbContext db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<NurseDashboardStatisticsDto> GetNurseDashboardAsync(int lowStockThreshold = 10, int cacheSeconds = 60)
    {
        var cacheKey = $"nurse-dashboard:{lowStockThreshold}";
        if (_cache.TryGetValue(cacheKey, out var cachedObj) && cachedObj is NurseDashboardStatisticsDto cached)
        {
            return cached;
        }

        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);
        var startOfWeek = StartOfWeek(today, DayOfWeek.Monday);

        // Parallelize the queries when possible
        var totalVaccineDosesTask = _db.Vacunas.AsNoTracking().SumAsync(v => (int?)v.DosisDisponibles) // nullable to handle empty set
            .ContinueWith(t => t.Result ?? 0);
        var totalSyringesTask = _db.Jeringas.AsNoTracking().SumAsync(j => (int?)j.CantidadDisponible)
            .ContinueWith(t => t.Result ?? 0);

        var applicationsTodayTask = _db.RegistrosVacunacion.AsNoTracking()
            .CountAsync(r => r.FechaAplicacion >= today && r.FechaAplicacion < tomorrow);

        var applicationsThisWeekTask = _db.RegistrosVacunacion.AsNoTracking()
            .CountAsync(r => r.FechaAplicacion >= startOfWeek && r.FechaAplicacion < tomorrow);

        var vaccinesBelowTask = _db.Vacunas.AsNoTracking().CountAsync(v => v.DosisDisponibles < lowStockThreshold);
        var syringesBelowTask = _db.Jeringas.AsNoTracking().CountAsync(j => j.CantidadDisponible < lowStockThreshold);
        await Task.WhenAll(totalVaccineDosesTask, totalSyringesTask,
            applicationsTodayTask, applicationsThisWeekTask,
            vaccinesBelowTask, syringesBelowTask);

        var dto = new NurseDashboardStatisticsDto
        {
            LastUpdated = today,
            TotalVaccineDoses = totalVaccineDosesTask.Result,
            TotalSyringes = totalSyringesTask.Result,
            ApplicationsToday = applicationsTodayTask.Result,
            ApplicationsThisWeek = applicationsThisWeekTask.Result,
            LowStock = new LowStockSummaryDto
            {
                Threshold = lowStockThreshold,
                VaccinesBelowThreshold = vaccinesBelowTask.Result,
                SyringesBelowThreshold = syringesBelowTask.Result
            }
        };

        _cache.Set(cacheKey, dto, TimeSpan.FromSeconds(cacheSeconds));
        return dto;
    }

    private static DateTime StartOfWeek(DateTime date, DayOfWeek startOfWeek)
    {
        int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
        return date.AddDays(-1 * diff).Date;
    }
}
