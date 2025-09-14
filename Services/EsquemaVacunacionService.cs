using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class EsquemaVacunacionService : IEsquemaVacunacionService
{
    private readonly IEsquemaVacunacionRepository _esquemaRepository;
    private readonly IVacunaRepository _vacunaRepository;
    private readonly IDiluyenteRepository _diluyenteRepository;
    private readonly IJeringaRepository _jeringaRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IAlarmaVacunacionService _alarmaService;

    public EsquemaVacunacionService(
        IEsquemaVacunacionRepository esquemaRepository,
        IVacunaRepository vacunaRepository,
        IDiluyenteRepository diluyenteRepository,
        IJeringaRepository jeringaRepository,
    IPacienteRepository pacienteRepository,
    IAlarmaVacunacionService alarmaService)
    {
        _esquemaRepository = esquemaRepository;
        _vacunaRepository = vacunaRepository;
        _diluyenteRepository = diluyenteRepository;
        _jeringaRepository = jeringaRepository;
        _pacienteRepository = pacienteRepository;
    _alarmaService = alarmaService;
    }

    public async Task RegistrarEsquemaAsync(EsquemaVacunacion esquemaVacunacion)
    {
        // Validar existencia de Paciente
        var paciente = await _pacienteRepository.GetByIdAsync(esquemaVacunacion.PacienteId);
        if (paciente == null)
            throw new InvalidOperationException($"PacienteId {esquemaVacunacion.PacienteId} no existe.");

        // Validar existencia de Vacuna principal del esquema
        var vacunaPrincipal = await _vacunaRepository.GetByIdAsync(esquemaVacunacion.VacunaId);
        if (vacunaPrincipal == null)
            throw new InvalidOperationException($"VacunaId {esquemaVacunacion.VacunaId} no existe.");

        // Transacción manual (contexto detrás de repos); se asume que todos repos usan mismo DbContext
        // Para minimizar cambios, si ocurre excepción en medio, se lanzará y se revertirá la transacción.
        var contextField = _esquemaRepository.GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dbContext = contextField?.GetValue(_esquemaRepository) as API.Persistence.Context.AplicationDbContext;
        if (dbContext == null)
        {
            // Fallback sin transacción explícita
            await ProcesarDetallesYGuardar(esquemaVacunacion);
            return;
        }

        using var trx = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await ProcesarDetallesYGuardar(esquemaVacunacion);
            await trx.CommitAsync();
        }
        catch
        {
            await trx.RollbackAsync();
            throw;
        }
    }

    private async Task ProcesarDetallesYGuardar(EsquemaVacunacion esquemaVacunacion)
    {
        // Normalizar fecha de aplicación (solo fecha, sin hora) ANTES de calcular próxima dosis
        esquemaVacunacion.FechaDosisAplicada = DateTime.UtcNow.Date;

        foreach (var detalle in esquemaVacunacion.Detalles)
        {
            if (detalle.VacunaId.HasValue)
            {
                var vac = await _vacunaRepository.GetByIdAsync(detalle.VacunaId.Value);
                if (vac == null) throw new InvalidOperationException($"Vacuna detalle Id {detalle.VacunaId.Value} no existe.");
                await _vacunaRepository.DescontarInventarioAsync(detalle.VacunaId.Value, detalle.CantidadUtilizadaVacuna ?? 0);
            }
            // Suero eliminado (equivalente a diluyente) – lógica removida
            if (detalle.DiluyenteId.HasValue)
            {
                var dil = await _diluyenteRepository.GetByIdAsync(detalle.DiluyenteId.Value);
                if (dil == null) throw new InvalidOperationException($"DiluyenteId {detalle.DiluyenteId.Value} no existe.");
                await _diluyenteRepository.DescontarInventarioAsync(detalle.DiluyenteId.Value, detalle.CantidadUtilizadaDiluyente ?? 0);
            }
            if (detalle.JeringaId.HasValue)
            {
                var jer = await _jeringaRepository.GetByIdAsync(detalle.JeringaId.Value);
                if (jer == null) throw new InvalidOperationException($"JeringaId {detalle.JeringaId.Value} no existe.");
                await _jeringaRepository.DescontarInventarioAsync(detalle.JeringaId.Value, detalle.CantidadUtilizadaJeringa ?? 0);
            }
        }
        // Calcular FechaProximaDosis usando solo componente fecha (si quedan dosis)
        var vacuna = await _vacunaRepository.GetByIdAsync(esquemaVacunacion.VacunaId);
        if (vacuna != null && esquemaVacunacion.NumeroDeDosis < vacuna.NumeroDosis)
        {
            var baseDate = esquemaVacunacion.FechaDosisAplicada.Date; // asegurar solo fecha
            esquemaVacunacion.FechaProximaDosis = baseDate.AddDays(vacuna.IntervaloSemanas * 7).Date;
        }
        else
        {
            esquemaVacunacion.FechaProximaDosis = null;
        }

        await _esquemaRepository.AddAsync(esquemaVacunacion);

        // Crear alarma sólo si hay próxima dosis
        if (esquemaVacunacion.FechaProximaDosis.HasValue)
        {
            await _alarmaService.CrearAlarmaDesdeEsquemaAsync(
                esquemaVacunacion.PacienteId,
                esquemaVacunacion.VacunaId,
                esquemaVacunacion.NumeroDeDosis,
                esquemaVacunacion.FechaDosisAplicada);
        }
    }

    public async Task<EsquemaVacunacion> GetEsquemaConDetallesAsync(int esquemaId)
    {
        return await _esquemaRepository.GetEsquemaConDetallesAsync(esquemaId);
    }

    public async Task<(bool aplica, int numeroDosis, string mensaje)> ValidarAplicacionDosisAsync(int pacienteId, int vacunaId)
    {
        // Obtener vacuna para reglas
        var vacuna = await _vacunaRepository.GetByIdAsync(vacunaId);
        if (vacuna == null)
            return (false, 0, "Vacuna no existe");

        // Traer último esquema registrado para ese paciente y vacuna
        var contextField = _esquemaRepository.GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dbContext = contextField?.GetValue(_esquemaRepository) as API.Persistence.Context.AplicationDbContext;
        EsquemaVacunacion ultimo = null;
        if (dbContext != null)
        {
            ultimo = dbContext.EsquemasVacunacion
                .Where(e => e.PacienteId == pacienteId && e.VacunaId == vacunaId)
                .OrderByDescending(e => e.FechaDosisAplicada)
                .FirstOrDefault();
        }

        // Si no hay registros todavía → primera dosis aplica inmediatamente
        if (ultimo == null)
        {
            return (true, 1, "Puede aplicarse la primera dosis");
        }

        // Si ya completó todas las dosis
        if (ultimo.NumeroDeDosis >= vacuna.NumeroDosis)
        {
            return (false, ultimo.NumeroDeDosis, "Esquema de vacunación finalizado");
        }

        // Calcular fecha próxima esperada (si no se guardó ya)
        var fechaProxima = ultimo.FechaProximaDosis;
        if (!fechaProxima.HasValue)
        {
            fechaProxima = ultimo.FechaDosisAplicada.Date.AddDays(vacuna.IntervaloSemanas * 7).Date;
        }

        var hoy = DateTime.UtcNow.Date;
        if (hoy < fechaProxima.Value.Date)
        {
            var faltan = (fechaProxima.Value.Date - hoy).Days;
            return (false, ultimo.NumeroDeDosis + 1, $"Todavía no corresponde. Faltan  {faltan} día(s) para la siguiente dosis.");
        }

        // Es el día o ya pasó la fecha → puede aplicarse siguiente dosis
        return (true, ultimo.NumeroDeDosis + 1, $"Debe aplicarse la dosis número {ultimo.NumeroDeDosis + 1}.");
    }

    public async Task<IEnumerable<API.DTO.EsquemaVacunacionListadoDto>> ListarEsquemasAsync()
    {
        // Acceder al DbContext subyacente del repo para construir una consulta con includes
        var contextField = _esquemaRepository.GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dbContext = contextField?.GetValue(_esquemaRepository) as API.Persistence.Context.AplicationDbContext;
        if (dbContext == null)
        {
            // Fallback simple: cargar todo y proyectar (menos eficiente)
            var todos = await _esquemaRepository.GetAllAsync();
            var resultados = new List<API.DTO.EsquemaVacunacionListadoDto>();
            foreach (var e in todos)
            {
                var paciente = await _pacienteRepository.GetByIdAsync(e.PacienteId);
                var vacuna = await _vacunaRepository.GetByIdAsync(e.VacunaId);
                resultados.Add(new API.DTO.EsquemaVacunacionListadoDto
                {
                    Id = e.Id,
                    TipoCarnet = e.TipoCarnet,
                    RegistradoPAI = e.RegistradoPAI,
                    NombreCompleto = paciente != null ? $"{paciente.PrimerNombre} {paciente.PrimerApellido}" : string.Empty,
                    VacunaAplicada = vacuna?.Nombre ?? string.Empty,
                    FechaAplicada = e.FechaDosisAplicada,
                    FechaProxima = e.FechaProximaDosis,
                    Responsable = e.Responsable
                });
            }
            return resultados;
        }

        // Consulta optimizada con includes
        var query = dbContext.EsquemasVacunacion
            .AsNoTracking()
            .Include(x => x.Paciente)
            .Include(x => x.Vacuna)
            .OrderByDescending(x => x.FechaDosisAplicada);

        var lista = await query
            .Select(e => new API.DTO.EsquemaVacunacionListadoDto
            {
                Id = e.Id,
                TipoCarnet = e.TipoCarnet,
                RegistradoPAI = e.RegistradoPAI,
                NombreCompleto = (e.Paciente.PrimerNombre + " " + e.Paciente.PrimerApellido).Trim(),
                VacunaAplicada = e.Vacuna.Nombre,
                FechaAplicada = e.FechaDosisAplicada,
                FechaProxima = e.FechaProximaDosis,
                Responsable = e.Responsable
            })
            .ToListAsync();

        return lista;
    }
}
