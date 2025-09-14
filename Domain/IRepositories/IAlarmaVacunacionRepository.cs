#nullable enable annotations
using API.Domain.Models;
using API.Domain.IRepositories;

namespace API.Domain.IRepositories
{
    public interface IAlarmaVacunacionRepository : IRepository<AlarmaVacunacion>
    {
        /// <summary>
        /// Obtiene las alarmas para vacunaciones del mes actual
        /// </summary>
        Task<IEnumerable<AlarmaVacunacion>> GetVacunacionesProximasMesActualAsync();
        
        /// <summary>
        /// Marca una alarma como notificada
        /// </summary>
        Task<bool> MarcarComoNotificadaAsync(int alarmaId);
        
        /// <summary>
        /// Obtiene las alarmas vencidas validando por semanas
        /// </summary>
        Task<IEnumerable<AlarmaVacunacion>> GetAlarmasVencidasPorSemanasAsync();
        
        /// <summary>
        /// Verifica si ya existe una alarma para el paciente y vacuna
        /// </summary>
        Task<bool> ExisteAlarmaPendienteAsync(int pacienteId, int vacunaId);
        /// <summary>
        /// Obtiene una alarma pendiente por paciente y vacuna
        /// </summary>
        Task<AlarmaVacunacion?> GetPendienteAsync(int pacienteId, int vacunaId);
    }
}