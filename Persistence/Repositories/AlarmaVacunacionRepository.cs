using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repositories
{
    public class AlarmaVacunacionRepository : Repository<AlarmaVacunacion>, IAlarmaVacunacionRepository
    {
        public AlarmaVacunacionRepository(AplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Obtiene las alarmas para vacunaciones del mes actual
        /// </summary>
        public async Task<IEnumerable<AlarmaVacunacion>> GetVacunacionesProximasMesActualAsync()
        {
            var inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var finMes = inicioMes.AddMonths(1).AddDays(-1);

            return await _context.AlarmasVacunacion
                .Include(a => a.Paciente)
                .Include(a => a.Vacuna)
                .Where(a => a.FechaProximaAplicacion >= inicioMes && 
                           a.FechaProximaAplicacion <= finMes &&
                           !a.NotificacionEnviada &&
                           !a.EsquemaCompletado)
                .OrderBy(a => a.FechaProximaAplicacion)
                .ToListAsync();
        }

        /// <summary>
        /// Marca una alarma como notificada
        /// </summary>
        public async Task<bool> MarcarComoNotificadaAsync(int alarmaId)
        {
            var alarma = await _context.AlarmasVacunacion.FindAsync(alarmaId);
            if (alarma != null)
            {
                alarma.NotificacionEnviada = true;
                alarma.FechaNotificacion = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Obtiene las alarmas vencidas validando por semanas
        /// </summary>
        public async Task<IEnumerable<AlarmaVacunacion>> GetAlarmasVencidasPorSemanasAsync()
        {
            // Consideramos vencida si ya pasó más de una semana de la fecha próxima aplicación
            var fechaLimiteVencimiento = DateTime.Now.AddDays(-7);

            return await _context.AlarmasVacunacion
                .Include(a => a.Paciente)
                .Include(a => a.Vacuna)
                .Where(a => !a.EsquemaCompletado && 
                           !a.NotificacionEnviada &&
                           a.FechaProximaAplicacion < fechaLimiteVencimiento)
                .OrderBy(a => a.FechaProximaAplicacion)
                .ToListAsync();
        }

        /// <summary>
        /// Verifica si ya existe una alarma para el paciente y vacuna
        /// </summary>
        public async Task<bool> ExisteAlarmaPendienteAsync(int pacienteId, int vacunaId)
        {
            return await _context.AlarmasVacunacion
                .AnyAsync(a => a.PacienteId == pacienteId && 
                              a.VacunaId == vacunaId && 
                              !a.EsquemaCompletado);
        }
    }
}