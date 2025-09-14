using API.Domain.Models;

namespace API.Domain.IServices
{
    public interface IEsquemaVacunacionService
    {
        Task RegistrarEsquemaAsync(EsquemaVacunacion esquemaVacunacion);
        Task<EsquemaVacunacion> GetEsquemaConDetallesAsync(int esquemaId);
    Task<(bool aplica, int numeroDosis, string mensaje)> ValidarAplicacionDosisAsync(int pacienteId, int vacunaId);
        Task<IEnumerable<API.DTO.EsquemaVacunacionListadoDto>> ListarEsquemasAsync();
    }
}