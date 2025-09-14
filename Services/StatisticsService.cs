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

        // Ejecutar consultas secuencialmente para evitar concurrencia en DbContext
        var totalVaccineDoses = (await _db.Vacunas.AsNoTracking().SumAsync(v => (int?)v.DosisDisponibles)) ?? 0;
        var totalSyringes = (await _db.Jeringas.AsNoTracking().SumAsync(j => (int?)j.CantidadDisponible)) ?? 0;
        var totalDiluents = (await _db.Diluyentes.AsNoTracking().SumAsync(d => (int?)d.CantidadDisponible)) ?? 0;

        var applicationsToday = await _db.RegistrosVacunacion.AsNoTracking()
            .CountAsync(r => r.FechaAplicacion >= today && r.FechaAplicacion < tomorrow);

        var applicationsThisWeek = await _db.RegistrosVacunacion.AsNoTracking()
            .CountAsync(r => r.FechaAplicacion >= startOfWeek && r.FechaAplicacion < tomorrow);

        var vaccinesBelow = await _db.Vacunas.AsNoTracking().CountAsync(v => v.DosisDisponibles < lowStockThreshold);
        var syringesBelow = await _db.Jeringas.AsNoTracking().CountAsync(j => j.CantidadDisponible < lowStockThreshold);

        var dto = new NurseDashboardStatisticsDto
        {
            LastUpdated = today,
            TotalVaccineDoses = totalVaccineDoses,
            TotalSyringes = totalSyringes,
            TotalDiluents = totalDiluents,
            ApplicationsToday = applicationsToday,
            ApplicationsThisWeek = applicationsThisWeek,
            LowStock = new LowStockSummaryDto
            {
                Threshold = lowStockThreshold,
                VaccinesBelowThreshold = vaccinesBelow,
                SyringesBelowThreshold = syringesBelow
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
