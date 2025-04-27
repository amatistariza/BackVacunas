using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;

namespace API.Persistence.Repositories
{
    public class CondicionUsuariaRepository : Repository<CondicionUsuaria>, ICondicionUsuariaRepository
    {
        public CondicionUsuariaRepository(AplicationDbContext context) : base(context)
        {
        }
    }
}
