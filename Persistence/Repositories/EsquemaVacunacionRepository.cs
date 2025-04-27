using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repositories
{
    public class EsquemaVacunacionRepository : Repository<EsquemaVacunacion>, IEsquemaVacunacionRepository
    {
        private readonly AplicationDbContext _context;

        public EsquemaVacunacionRepository(AplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EsquemaVacunacion> GetEsquemaConDetallesAsync(int esquemaId)
        {
            return await _context.EsquemasVacunacion
                .Include(e => e.Detalles)
                    .ThenInclude(d => d.Vacuna)
                .Include(e => e.Detalles)
                    .ThenInclude(d => d.Suero)
                .Include(e => e.Detalles)
                    .ThenInclude(d => d.Diluyente)
                .Include(e => e.Detalles)
                    .ThenInclude(d => d.Jeringa)
                .FirstOrDefaultAsync(e => e.Id == esquemaId);
        }
    }
}
