using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;
using API.Services;

namespace API.Persistence.Repositories
{
    public class CondicionUsuariaService : BaseService<CondicionUsuaria>, ICondicionUsuariaService
    {
        public CondicionUsuariaService(ICondicionUsuariaRepository condicionUsuariaRepository) : base(condicionUsuariaRepository)
        {
        }

        // Métodos específicos para CondicionUsuaria (si son necesarios)
    }
}
