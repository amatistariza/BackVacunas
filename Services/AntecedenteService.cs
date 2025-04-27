using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;

namespace API.Services
{
    public class AntecedenteService : BaseService<Antecedente>, IAntecedenteService
    {
        public AntecedenteService(IAntecedenteRepository antecedenteRepository) : base(antecedenteRepository)
        {
        }

        // Métodos específicos para Antecedentes, si son necesarios
    }
}
