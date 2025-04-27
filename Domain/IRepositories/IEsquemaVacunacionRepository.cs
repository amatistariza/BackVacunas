using API.Domain.Models;

namespace API.Domain.IRepositories
{
    public interface IEsquemaVacunacionRepository : IRepository<EsquemaVacunacion>
    {
        Task<EsquemaVacunacion> GetEsquemaConDetallesAsync(int esquemaId);
    }
}
