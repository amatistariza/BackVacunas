using API.Domain.Models;

namespace API.Domain.IServices
{
    public interface IEsquemaVacunacionService
    {
        Task RegistrarEsquemaAsync(EsquemaVacunacion esquemaVacunacion);
        Task<EsquemaVacunacion> GetEsquemaConDetallesAsync(int esquemaId);
    }
}