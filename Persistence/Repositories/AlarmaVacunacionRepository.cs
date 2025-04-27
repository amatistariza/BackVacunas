using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repositories
{
    public class AlarmaVacunacionRepository : Repository<AlarmaVacunacion>, IAlarmaVacunacionRepository
    {
        private readonly AplicationDbContext _context;

        public AlarmaVacunacionRepository(AplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlarmaVacunacion>> GetAlarmasByPacienteAsync(int pacienteId)
        {
            return await _context.AlarmasVacunacion
                .Include(a => a.Vacuna)
                .Where(a => a.PacienteId == pacienteId && !a.EsquemaCompletado)
                .OrderBy(a => a.FechaProximaAplicacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlarmaVacunacion>> GetAlarmasPendientesAsync()
        {
            var fechaInicioMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var fechaFinMes = fechaInicioMes.AddMonths(1).AddDays(-1);

            return await _context.AlarmasVacunacion
                .Include(a => a.Paciente)
                .Include(a => a.Vacuna)
                .Where(a => a.FechaProximaAplicacion >= fechaInicioMes && 
                           a.FechaProximaAplicacion <= fechaFinMes &&
                           !a.NotificacionEnviada &&
                           !a.EsquemaCompletado)
                .OrderBy(a => a.FechaProximaAplicacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlarmaVacunacion>> GetAlarmasByVacunaAsync(int vacunaId)
        {
            return await _context.AlarmasVacunacion
                .Include(a => a.Paciente)
                .Where(a => a.VacunaId == vacunaId && !a.EsquemaCompletado)
                .OrderBy(a => a.FechaProximaAplicacion)
                .ToListAsync();
        }

        public async Task MarcarComoNotificadaAsync(int alarmaId)
        {
            var alarma = await _context.AlarmasVacunacion.FindAsync(alarmaId);
            if (alarma != null)
            {
                alarma.NotificacionEnviada = true;
                alarma.FechaNotificacion = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompletarEsquemaAsync(int alarmaId)
        {
            var alarma = await _context.AlarmasVacunacion.FindAsync(alarmaId);
            if (alarma != null)
            {
                alarma.EsquemaCompletado = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}