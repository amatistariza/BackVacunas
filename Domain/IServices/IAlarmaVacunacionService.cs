using API.Domain.Models;

namespace API.Domain.IServices
{
    public interface IAlarmaVacunacionService
    {
        /// <summary>
        /// Obtiene las vacunaciones próximas del mes actual
        /// </summary>
        Task<IEnumerable<AlarmaVacunacion>> GetVacunacionesProximasMesActualAsync();
        
        /// <summary>
        /// Marca una alarma como notificada (ya se notificó al paciente)
        /// </summary>
        Task<bool> MarcarComoNotificadaAsync(int alarmaId);
        
        /// <summary>
        /// Obtiene las alarmas vencidas validando por semanas
        /// </summary>
        Task<IEnumerable<AlarmaVacunacion>> GetAlarmasVencidasPorSemanasAsync();
        
        /// <summary>
        /// Crea alarmas automáticamente cuando se llama al endpoint de esquema vacunación
        /// </summary>
    Task CrearAlarmaDesdeEsquemaAsync(int pacienteId, int vacunaId, int numeroDosiActual, DateTime fechaUltimaAplicacion, DateTime? fechaProximaAplicacion = null);
    }
}