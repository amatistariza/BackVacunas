using API.Domain.Models;

namespace API.Domain.IRepositories
{
    public interface IAlarmaVacunacionRepository : IRepository<AlarmaVacunacion>
    {
        // Buscar alarmas para un paciente específico
        Task<IEnumerable<AlarmaVacunacion>> GetAlarmasByPacienteAsync(int pacienteId);
        
        // Buscar alarmas pendientes (próximas a vencer en el mes actual)
        Task<IEnumerable<AlarmaVacunacion>> GetAlarmasPendientesAsync();
        
        // Buscar alarmas por vacuna
        Task<IEnumerable<AlarmaVacunacion>> GetAlarmasByVacunaAsync(int vacunaId);
        
        // Marcar una alarma como notificada
        Task MarcarComoNotificadaAsync(int alarmaId);
        
        // Marcar un esquema como completado
        Task CompletarEsquemaAsync(int alarmaId);
    }
}