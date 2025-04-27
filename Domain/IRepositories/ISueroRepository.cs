using API.Domain.Models.Esquema;

namespace API.Domain.IRepositories;

public interface ISueroRepository : IRepository<Suero>
{
    Task DescontarInventarioAsync(int sueroId, int cantidad);
}