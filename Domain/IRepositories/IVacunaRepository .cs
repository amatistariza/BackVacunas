using API.Domain.Models.Esquema;

namespace API.Domain.IRepositories
{
    public interface IVacunaRepository : IRepository<Vacuna>
    {
        Task DescontarInventarioAsync(int vacunaId, int cantidad);
    }
}
