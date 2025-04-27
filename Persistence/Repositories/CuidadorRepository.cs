using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;

namespace API.Persistence.Repositories
{
    public class CuidadorRepository : Repository<Cuidador>, ICuidadorRepository
    {
        public CuidadorRepository(AplicationDbContext context) : base(context)
        {
        }
    }
}
