using API.Domain.IRepositories;
using API.Domain.Models.Esquema;
using API.Persistence.Context;

namespace API.Persistence.Repositories;

public class VacunaRepository : Repository<Vacuna>, IVacunaRepository
{
    private readonly AplicationDbContext _context;

    public VacunaRepository(AplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task DescontarInventarioAsync(int vacunaId, int cantidad)
    {
        var vacuna = await _context.Vacunas.FindAsync(vacunaId);
        if (vacuna == null || vacuna.DosisDisponibles < cantidad)
            throw new InvalidOperationException("No hay suficientes dosis disponibles de la vacuna.");

        vacuna.DosisDisponibles -= cantidad;
        await _context.SaveChangesAsync();
    }
}