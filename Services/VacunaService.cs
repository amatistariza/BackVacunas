using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models.Esquema;

namespace API.Services
{
    public class VacunaService : BaseService<Vacuna>, IVacunaService
    {
        public VacunaService(IRepository<Vacuna> repository) : base(repository)
        {
        }

        // Métodos específicos de vacunas (si es necesario)
    }
}
