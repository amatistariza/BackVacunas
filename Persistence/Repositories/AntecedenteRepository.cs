using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;

namespace API.Persistence.Repositories
{
    public class AntecedenteRepository : Repository<Antecedente>, IAntecedenteRepository
    {
        public AntecedenteRepository(AplicationDbContext context) : base(context)
        {
        }
    }
}
