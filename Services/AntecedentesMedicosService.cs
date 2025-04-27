using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;

namespace API.Services
{
    public class AntecedentesMedicosService : BaseService<AntecedentesMedicos>, IAntecedentesMedicosService
    {
        public AntecedentesMedicosService(IAntecedentesMedicosRepository antecedentesMedicosRepository)
            : base(antecedentesMedicosRepository)
        {
        }
    }

}
