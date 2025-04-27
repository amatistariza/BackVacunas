using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;

namespace API.Persistence.Repositories
{
    public class AntecedentesMedicosRepository : Repository<AntecedentesMedicos>, IAntecedentesMedicosRepository
    {
        public AntecedentesMedicosRepository(AplicationDbContext context) : base(context)
        {
        }
    }
}
