using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;

namespace API.Services
{
    public class CuidadorService : BaseService<Cuidador>, ICuidadorService
    {
        public CuidadorService(ICuidadorRepository cuidadorRepository) : base(cuidadorRepository)
        {
        }

        // Métodos específicos para Cuidador (si son necesarios en el futuro)
    }
}
