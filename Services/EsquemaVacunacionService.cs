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
    private readonly IAlarmaVacunacionService _alarmaService;

    public EsquemaVacunacionService(
        IEsquemaVacunacionRepository esquemaRepository,
        IVacunaRepository vacunaRepository,
        ISueroRepository sueroRepository,
        IDiluyenteRepository diluyenteRepository,
        IJeringaRepository jeringaRepository,
        IAlarmaVacunacionService alarmaService)
    {
        _esquemaRepository = esquemaRepository;
        _vacunaRepository = vacunaRepository;
        _sueroRepository = sueroRepository;
        _diluyenteRepository = diluyenteRepository;
        _jeringaRepository = jeringaRepository;
        _alarmaService = alarmaService;
    }

    public async Task RegistrarEsquemaAsync(EsquemaVacunacion esquemaVacunacion)
    {
        foreach (var detalle in esquemaVacunacion.Detalles)
        {
            if (detalle.VacunaId.HasValue)
            {
                await _vacunaRepository.DescontarInventarioAsync(detalle.VacunaId.Value, detalle.CantidadUtilizadaVacuna ?? 0);
                
                // Crear alarma para la vacuna si tiene múltiples dosis
                var vacuna = await _vacunaRepository.GetByIdAsync(detalle.VacunaId.Value);
                if (vacuna != null && vacuna.NumeroDosis > 1)
                {
                    // Registrar la alarma para la próxima aplicación
                    await _alarmaService.CrearAlarmaAsync(
                        esquemaVacunacion.PacienteId,
                        detalle.VacunaId.Value,
                        detalle.NumeroDosis, // Número de dosis actual
                        detalle.FechaAplicacion // Fecha de la aplicación actual
                    );
                }
            }

            if (detalle.SueroId.HasValue)
                await _sueroRepository.DescontarInventarioAsync(detalle.SueroId.Value, detalle.CantidadUtilizadaSuero ?? 0);

            if (detalle.DiluyenteId.HasValue)
                await _diluyenteRepository.DescontarInventarioAsync(detalle.DiluyenteId.Value, detalle.CantidadUtilizadaDiluyente ?? 0);

            if (detalle.JeringaId.HasValue)
                await _jeringaRepository.DescontarInventarioAsync(detalle.JeringaId.Value, detalle.CantidadUtilizadaJeringa ?? 0);
        }

        await _esquemaRepository.AddAsync(esquemaVacunacion);
    }

    public async Task<EsquemaVacunacion> GetEsquemaConDetallesAsync(int esquemaId)
    {
        return await _esquemaRepository.GetEsquemaConDetallesAsync(esquemaId);
    }
}
