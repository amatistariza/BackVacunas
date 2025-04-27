using API.Domain.Models.Esquema;

namespace API.Domain.IRepositories;

public interface IDiluyenteRepository : IRepository<Diluyente>
{
    Task DescontarInventarioAsync(int diluyenteId, int cantidad);
}
