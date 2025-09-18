using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repositories
{
    public class MadreRepository : Repository<Madre>, IMadreRepository
    {
        public MadreRepository(AplicationDbContext context) : base(context)
        {
        }

    public async Task<Madre> GetByIdentificacionAsync(string tipoIdentificacion, string numeroIdentificacion)
        {
            var tipo = (tipoIdentificacion ?? string.Empty).Trim().ToUpperInvariant();
            var numero = (numeroIdentificacion ?? string.Empty).Trim();
            return await _context.Madres
                .AsNoTracking()
        .FirstOrDefaultAsync(m => m.TipoIdentificacion == tipo && m.NumeroIdentificacion == numero);
        }
    }
}