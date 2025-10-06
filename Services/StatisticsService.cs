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
                Dosis = g.Count(),
                FechaAplicacion = g.Max(e => e.FechaDosisAplicada)
            })
            .OrderByDescending(x => x.Dosis)
            .ThenBy(x => x.Vacuna);

        var list = await query.ToListAsync();
        _cache.Set(cacheKey, list, TimeSpan.FromSeconds(cacheSeconds));
        return list;
    }

    public async Task<IReadOnlyList<VacunaAplicadaDetalleDto>> GetVacunasAplicadasAsync(DateTime? desde = null, DateTime? hasta = null, int cacheSeconds = 60)
    {
        // Rango [start, end)
        var start = (desde ?? DateTime.MinValue).Date;
        var end = ((hasta ?? DateTime.Today).Date).AddDays(1);

        var cacheKey = $"vaccines-applied:{start:yyyyMMdd}:{end:yyyyMMdd}";
        if (_cache.TryGetValue(cacheKey, out var cachedObj) && cachedObj is IReadOnlyList<VacunaAplicadaDetalleDto> cached)
        {
            return cached;
        }

        // Traer esquemas con Paciente y Vacuna para construir DTO
        var esquemas = await _db.EsquemasVacunacion
            .AsNoTracking()
            .Include(e => e.Paciente)
            .Include(e => e.Vacuna)
            .Where(e => e.FechaDosisAplicada >= start && e.FechaDosisAplicada < end)
            .OrderByDescending(e => e.FechaDosisAplicada)
            .ThenBy(e => e.Vacuna.Nombre)
            .ThenBy(e => e.NumeroDeDosis)
            .ToListAsync();

        // Procesar en memoria para calcular edad en días, meses o años
        var result = esquemas.Select(e =>
        {
            var edadEnAnios = CalcularEdadEnAnios(e.Paciente.FechaNacimiento, e.FechaDosisAplicada);
            var edad = edadEnAnios;
            var unidadEdad = "años";

            // Si es menor a 1 año, calcular en meses o días
            if (edadEnAnios < 1)
            {
                var edadEnMeses = CalcularEdadEnMeses(e.Paciente.FechaNacimiento, e.FechaDosisAplicada);
                
                // Si es menor a 1 mes, calcular en días
                if (edadEnMeses < 1)
                {
                    edad = CalcularEdadEnDias(e.Paciente.FechaNacimiento, e.FechaDosisAplicada);
                    unidadEdad = "días";
                }
                else
                {
                    edad = edadEnMeses;
                    unidadEdad = "meses";
                }
            }

            return new VacunaAplicadaDetalleDto
            {
                Vacuna = e.Vacuna.Nombre,
                NumeroDosis = e.NumeroDeDosis,
                TipoIdentificacion = e.Paciente.TipoIdentificacion,
                NumeroIdentificacion = e.Paciente.NumeroIdentificacion,
                Nombre = e.Paciente.PrimerNombre,
                Apellido = e.Paciente.PrimerApellido,
                Edad = edad,
                UnidadEdad = unidadEdad,
                FechaAplicacion = e.FechaDosisAplicada,
                // Campos adicionales del paciente
                RegimenAfiliacion = e.Paciente.RegimenAfiliacion,
                PertenenciaEtnica = e.Paciente.PertenenciaEtnica,
                Sexo = e.Paciente.Sexo,
                Desplazado = e.Paciente.Desplazado,
                Discapacitado = e.Paciente.Discapacitado,
                VictimaConflicto = e.Paciente.VictimaConflictoArmado,
                EstudiaActualmente = e.Paciente.EstudiaActualmente
            };
        }).ToList();
        _cache.Set(cacheKey, result, TimeSpan.FromSeconds(cacheSeconds));
        return result;
    }

    // Métodos auxiliares para calcular edad
    private static int CalcularEdadEnAnios(DateTime fechaNacimiento, DateTime fechaReferencia)
    {
        var edad = fechaReferencia.Year - fechaNacimiento.Year;
        if (fechaReferencia < fechaNacimiento.AddYears(edad))
        {
            edad--;
        }
        return edad;
    }

    private static int CalcularEdadEnMeses(DateTime fechaNacimiento, DateTime fechaReferencia)
    {
        var meses = (fechaReferencia.Year - fechaNacimiento.Year) * 12;
        meses += fechaReferencia.Month - fechaNacimiento.Month;
        
        // Ajustar si el día de referencia es menor al día de nacimiento
        if (fechaReferencia.Day < fechaNacimiento.Day)
        {
            meses--;
        }
        
        return Math.Max(0, meses); // Nunca negativo
    }

    private static int CalcularEdadEnDias(DateTime fechaNacimiento, DateTime fechaReferencia)
    {
        var dias = (fechaReferencia.Date - fechaNacimiento.Date).Days;
        return Math.Max(0, dias); // Nunca negativo
    }
}
