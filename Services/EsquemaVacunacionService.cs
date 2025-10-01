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

        // Verificar si el esquema ya está completo para este paciente y vacuna
        var contextFieldPre = _esquemaRepository.GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dbContextPre = contextFieldPre?.GetValue(_esquemaRepository) as API.Persistence.Context.AplicationDbContext;
        if (dbContextPre != null)
        {
            var maxDosisActual = await dbContextPre.EsquemasVacunacion
                .Where(e => e.PacienteId == esquemaVacunacion.PacienteId && e.VacunaId == esquemaVacunacion.VacunaId)
                .Select(e => (int?)e.NumeroDeDosis)
                .MaxAsync();
            if ((maxDosisActual ?? 0) >= vacunaPrincipal.NumeroDosis)
            {
                throw new InvalidOperationException("El esquema de vacunación ya está completo para esta vacuna.");
            }
        }

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

        // Usar transacción solo si el proveedor es relacional; InMemory no soporta transacciones
        if (!dbContext.Database.IsRelational())
        {
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
    // Normalizar fecha de aplicación (solo fecha local, sin hora) ANTES de calcular próxima dosis
    esquemaVacunacion.FechaDosisAplicada = DateTime.Today;

        // Calcular NumeroDeDosis en servidor según historial
        var contextField = _esquemaRepository.GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dbContext = contextField?.GetValue(_esquemaRepository) as API.Persistence.Context.AplicationDbContext;
        if (dbContext != null)
        {
            // Tomar la máxima dosis registrada para evitar empates por fecha
            var maxDosis = await dbContext.EsquemasVacunacion
                .Where(e => e.PacienteId == esquemaVacunacion.PacienteId && e.VacunaId == esquemaVacunacion.VacunaId)
                .Select(e => (int?)e.NumeroDeDosis)
                .MaxAsync();
            esquemaVacunacion.NumeroDeDosis = (maxDosis ?? 0) + 1;
        }

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
            // Última dosis aplicada: no hay próxima dosis
            esquemaVacunacion.FechaProximaDosis = null;
        }

        await _esquemaRepository.AddAsync(esquemaVacunacion);

        // Si hay próxima dosis: crear/actualizar alarma; si no, marcar alarmas como completadas
        if (esquemaVacunacion.FechaProximaDosis.HasValue)
        {
            await _alarmaService.CrearAlarmaDesdeEsquemaAsync(
                esquemaVacunacion.PacienteId,
                esquemaVacunacion.VacunaId,
                esquemaVacunacion.NumeroDeDosis,
                esquemaVacunacion.FechaDosisAplicada,
                esquemaVacunacion.FechaProximaDosis);
        }
        else if (vacuna != null && esquemaVacunacion.NumeroDeDosis >= vacuna.NumeroDosis)
        {
            // Marcar cualquier alarma pendiente como esquema completado
            await _alarmaService.MarcarEsquemaCompletadoAsync(esquemaVacunacion.PacienteId, esquemaVacunacion.VacunaId);
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

        // Consultar contexto para calcular de forma determinística
        var contextField = _esquemaRepository.GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dbContext = contextField?.GetValue(_esquemaRepository) as API.Persistence.Context.AplicationDbContext;
        if (dbContext == null)
        {
            // Fallback: sin contexto, permitir primera dosis
            return (true, 1, "Puede aplicarse la primera dosis");
        }

        // Calcular máxima dosis aplicada
        var maxDosis = await dbContext.EsquemasVacunacion
            .Where(e => e.PacienteId == pacienteId && e.VacunaId == vacunaId)
            .Select(e => (int?)e.NumeroDeDosis)
            .MaxAsync();

        if (maxDosis == null)
        {
            return (true, 1, "Puede aplicarse la primera dosis");
        }

        if (maxDosis.Value >= vacuna.NumeroDosis)
        {
            return (false, maxDosis.Value, "Esquema de vacunación finalizado");
        }

        // Traer el último registro correspondiente a la dosis máxima para calcular la fecha próxima
        var ultimo = await dbContext.EsquemasVacunacion
            .Where(e => e.PacienteId == pacienteId && e.VacunaId == vacunaId && e.NumeroDeDosis == maxDosis.Value)
            .OrderByDescending(e => e.FechaDosisAplicada)
            .FirstOrDefaultAsync();

        var fechaProxima = ultimo?.FechaProximaDosis;
        if (!fechaProxima.HasValue)
        {
            var baseDate = (ultimo?.FechaDosisAplicada ?? DateTime.Today).Date;
            fechaProxima = baseDate.AddDays(vacuna.IntervaloSemanas * 7).Date;
        }

        var hoy = DateTime.Today;
        if (hoy < fechaProxima.Value.Date)
        {
            var faltan = (fechaProxima.Value.Date - hoy).Days;
            return (false, maxDosis.Value + 1, $"Todavía no corresponde. Faltan  {faltan} día(s) para la siguiente dosis.");
        }

        // Es el día o ya pasó la fecha → puede aplicarse siguiente dosis
        return (true, maxDosis.Value + 1, $"Debe aplicarse la dosis número {maxDosis.Value + 1}.");
    }

    public async Task<IEnumerable<API.DTO.EsquemaVacunacionListadoDto>> ListarEsquemasAsync(string identificacion = null)
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
                if (identificacion != null && paciente != null && !string.Equals(paciente.NumeroIdentificacion, identificacion, StringComparison.OrdinalIgnoreCase))
                    continue;

                resultados.Add(new API.DTO.EsquemaVacunacionListadoDto
                {
                    EsquemaId = e.Id,
                    TipoCarnet = e.TipoCarnet,
                    RegistradoPAI = e.RegistradoPAI,
                    TipoIdentificacion = paciente?.TipoIdentificacion ?? string.Empty,
                    NumeroIdentificacion = paciente?.NumeroIdentificacion ?? string.Empty,
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

        if (!string.IsNullOrEmpty(identificacion))
        {
            query = query.Where(e => e.Paciente.NumeroIdentificacion == identificacion)
                         .OrderByDescending(x => x.FechaDosisAplicada);
        }

        var lista = await query
            .Select(e => new API.DTO.EsquemaVacunacionListadoDto
            {
                EsquemaId = e.Id,
                TipoCarnet = e.TipoCarnet,
                RegistradoPAI = e.RegistradoPAI,
                TipoIdentificacion = e.Paciente.TipoIdentificacion,
                NumeroIdentificacion = e.Paciente.NumeroIdentificacion,
                VacunaAplicada = e.Vacuna.Nombre,
                FechaAplicada = e.FechaDosisAplicada,
                FechaProxima = e.FechaProximaDosis,
                Responsable = e.Responsable
            })
            .ToListAsync();

        return lista;
    }
}
