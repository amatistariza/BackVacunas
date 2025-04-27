using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models.Esquema;

namespace API.Services
{
    public class DiluyenteService : BaseService<Diluyente>, IDiluyenteService
    {
        public DiluyenteService(IRepository<Diluyente> repository) : base(repository)
        {
        }

        // Métodos específicos de diluyentes (si es necesario)
    }
}
