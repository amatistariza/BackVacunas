using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;

namespace API.Persistence.Repositories
{
    public class MadreRepository : Repository<Madre>, IMadreRepository
    {
        public MadreRepository(AplicationDbContext context) : base(context)
        {
        }
    }
}