using API.Domain.Models;

namespace API.Domain.IServices
{
    public interface IAlarmaVacunacionService
    {
        // Crear una nueva alarma cuando se registra una dosis
        Task CrearAlarmaAsync(int pacienteId, int vacunaId, int dosisActual, DateTime fechaAplicacion);
        
        // Obtener alarmas pendientes para un paciente
        Task<IEnumerable<AlarmaVacunacion>> GetAlarmasByPacienteAsync(int pacienteId);
        
        // Obtener alarmas que vencen en el mes actual
        Task<IEnumerable<AlarmaVacunacion>> GetAlarmasPendientesAsync();
        
        // Obtener alarmas por tipo de vacuna
        Task<IEnumerable<AlarmaVacunacion>> GetAlarmasByVacunaAsync(int vacunaId);
        
        // Marcar una alarma como notificada
        Task MarcarComoNotificadaAsync(int alarmaId);
        
        // Registrar la aplicación de la siguiente dosis
        Task RegistrarSiguienteDosisAsync(int alarmaId, DateTime fechaAplicacion);
        
        // Marcar un esquema como completado
        Task CompletarEsquemaAsync(int alarmaId);
    }
}