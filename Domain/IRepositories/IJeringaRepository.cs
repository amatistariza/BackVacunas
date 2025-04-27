using API.Domain.Models.Esquema;

namespace API.Domain.IRepositories
{
    public interface IJeringaRepository : IRepository<Jeringa>
    {
        Task DescontarInventarioAsync(int jeringaId, int cantidad);
    }
}
