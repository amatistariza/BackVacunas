using API.Domain.IRepositories;
using API.Domain.Models.Esquema;
using API.Persistence.Context;

namespace API.Persistence.Repositories;

public class JeringaRepository : Repository<Jeringa>, IJeringaRepository
{
    private readonly AplicationDbContext _context;

    public JeringaRepository(AplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task DescontarInventarioAsync(int jeringaId, int cantidad)
    {
        var jeringa = await _context.Jeringas.FindAsync(jeringaId);
        if (jeringa == null || jeringa.CantidadDisponible < cantidad)
            throw new InvalidOperationException("No hay suficientes jeringas disponibles.");

        jeringa.CantidadDisponible -= cantidad;
        await _context.SaveChangesAsync();
    }
}
