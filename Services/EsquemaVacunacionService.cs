using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;

namespace API.Services;

public class EsquemaVacunacionService : IEsquemaVacunacionService
{
    private readonly IEsquemaVacunacionRepository _esquemaRepository;
    private readonly IVacunaRepository _vacunaRepository;
    private readonly ISueroRepository _sueroRepository;
    private readonly IDiluyenteRepository _diluyenteRepository;
    private readonly IJeringaRepository _jeringaRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IAlarmaVacunacionService _alarmaService;

    public EsquemaVacunacionService(
        IEsquemaVacunacionRepository esquemaRepository,
        IVacunaRepository vacunaRepository,
        ISueroRepository sueroRepository,
        IDiluyenteRepository diluyenteRepository,
        IJeringaRepository jeringaRepository,
    IPacienteRepository pacienteRepository,
    IAlarmaVacunacionService alarmaService)
    {
        _esquemaRepository = esquemaRepository;
        _vacunaRepository = vacunaRepository;
        _sueroRepository = sueroRepository;
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
        foreach (var detalle in esquemaVacunacion.Detalles)
        {
            if (detalle.VacunaId.HasValue)
            {
                var vac = await _vacunaRepository.GetByIdAsync(detalle.VacunaId.Value);
                if (vac == null) throw new InvalidOperationException($"Vacuna detalle Id {detalle.VacunaId.Value} no existe.");
                await _vacunaRepository.DescontarInventarioAsync(detalle.VacunaId.Value, detalle.CantidadUtilizadaVacuna ?? 0);
                await _alarmaService.CrearAlarmaDesdeEsquemaAsync(
                    esquemaVacunacion.PacienteId,
                    detalle.VacunaId.Value,
                    detalle.NumeroDosis,
                    detalle.FechaAplicacion);
            }
            if (detalle.SueroId.HasValue)
            {
                var suero = await _sueroRepository.GetByIdAsync(detalle.SueroId.Value);
                if (suero == null) throw new InvalidOperationException($"SueroId {detalle.SueroId.Value} no existe.");
                await _sueroRepository.DescontarInventarioAsync(detalle.SueroId.Value, detalle.CantidadUtilizadaSuero ?? 0);
            }
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
        await _esquemaRepository.AddAsync(esquemaVacunacion);
    }

    public async Task<EsquemaVacunacion> GetEsquemaConDetallesAsync(int esquemaId)
    {
        return await _esquemaRepository.GetEsquemaConDetallesAsync(esquemaId);
    }
}
