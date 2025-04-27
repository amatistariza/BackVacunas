using API.Domain.IRepositories;
using API.Domain.Models.Esquema;
using API.Persistence.Context;

namespace API.Persistence.Repositories;

public class SueroRepository : Repository<Suero>, ISueroRepository
{
    private readonly AplicationDbContext _context;

    public SueroRepository(AplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task DescontarInventarioAsync(int sueroId, int cantidad)
    {
        var suero = await _context.Sueros.FindAsync(sueroId);
        if (suero == null || suero.FrascosDisponibles < cantidad)
            throw new InvalidOperationException("No hay suficientes frascos disponibles del suero.");

        suero.FrascosDisponibles -= cantidad;
        await _context.SaveChangesAsync();
    }
}
