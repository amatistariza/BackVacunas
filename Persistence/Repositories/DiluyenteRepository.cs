using API.Domain.IRepositories;
using API.Domain.Models.Esquema;
using API.Persistence.Context;

namespace API.Persistence.Repositories;

public class DiluyenteRepository : Repository<Diluyente>, IDiluyenteRepository
{
    private readonly AplicationDbContext _context;

    public DiluyenteRepository(AplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task DescontarInventarioAsync(int diluyenteId, int cantidad)
    {
        var diluyente = await _context.Diluyentes.FindAsync(diluyenteId);
        if (diluyente == null || diluyente.CantidadDisponible < cantidad)
            throw new InvalidOperationException("No hay suficientes diluyentes disponibles.");

        diluyente.CantidadDisponible -= cantidad;
        await _context.SaveChangesAsync();
    }
}
