using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repositories
{
    public class EsquemaVacunacionRepository : Repository<EsquemaVacunacion>, IEsquemaVacunacionRepository
    {
        public EsquemaVacunacionRepository(AplicationDbContext context) : base(context)
        {
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
                .Include(e => e.Paciente)
                .Include(e => e.Vacuna)
                .FirstOrDefaultAsync(e => e.Id == esquemaId);
        }
    }
}
