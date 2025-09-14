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

    var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
    var startOfWeek = StartOfWeek(today, DayOfWeek.Monday);
    var startOfMonth = new DateTime(today.Year, today.Month, 1);

        // Ejecutar consultas secuencialmente para evitar concurrencia en DbContext
        var totalVaccineDoses = (await _db.Vacunas.AsNoTracking().SumAsync(v => (int?)v.DosisDisponibles)) ?? 0;
        var totalSyringes = (await _db.Jeringas.AsNoTracking().SumAsync(j => (int?)j.CantidadDisponible)) ?? 0;
        var totalDiluents = (await _db.Diluyentes.AsNoTracking().SumAsync(d => (int?)d.CantidadDisponible)) ?? 0;

        var applicationsToday = await _db.EsquemasVacunacion.AsNoTracking()
            .CountAsync(e => e.FechaDosisAplicada >= today && e.FechaDosisAplicada < tomorrow);

        var applicationsThisWeek = await _db.EsquemasVacunacion.AsNoTracking()
            .CountAsync(e => e.FechaDosisAplicada >= startOfWeek && e.FechaDosisAplicada < tomorrow);

        var applicationsThisMonth = await _db.EsquemasVacunacion.AsNoTracking()
            .CountAsync(e => e.FechaDosisAplicada >= startOfMonth && e.FechaDosisAplicada < tomorrow);

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
            ApplicationsThisMonth = applicationsThisMonth,
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

    public async Task<IReadOnlyList<DosisPorVacunaDto>> GetDosesPerVaccineAsync(DateTime? desde = null, DateTime? hasta = null, int cacheSeconds = 60)
    {
        // Normalizar rango: [inicio, fin)
        var start = (desde ?? DateTime.MinValue).Date;
        var end = ((hasta ?? DateTime.Today).Date).AddDays(1);

        var cacheKey = $"doses-per-vaccine:{start:yyyyMMdd}:{end:yyyyMMdd}";
        if (_cache.TryGetValue(cacheKey, out var cachedObj) && cachedObj is IReadOnlyList<DosisPorVacunaDto> cached)
        {
            return cached;
        }

        // Query en EsquemasVacunacion, agrupando por nombre de vacuna
        var query = _db.EsquemasVacunacion
            .AsNoTracking()
            .Include(e => e.Vacuna)
            .Where(e => e.FechaDosisAplicada >= start && e.FechaDosisAplicada < end)
            .GroupBy(e => e.Vacuna.Nombre)
            .Select(g => new DosisPorVacunaDto
            {
                Vacuna = g.Key,
                Dosis = g.Count()
            })
            .OrderByDescending(x => x.Dosis)
            .ThenBy(x => x.Vacuna);

        var list = await query.ToListAsync();
        _cache.Set(cacheKey, list, TimeSpan.FromSeconds(cacheSeconds));
        return list;
    }
}
