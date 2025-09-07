using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;

namespace API.Services
{
    public class AlarmaVacunacionService : IAlarmaVacunacionService
    {
        private readonly IAlarmaVacunacionRepository _alarmaRepository;
        private readonly IVacunaRepository _vacunaRepository;

        public AlarmaVacunacionService(
            IAlarmaVacunacionRepository alarmaRepository,
            IVacunaRepository vacunaRepository)
        {
            _alarmaRepository = alarmaRepository;
            _vacunaRepository = vacunaRepository;
        }

        /// <summary>
        /// Obtiene las vacunaciones próximas del mes actual
        /// </summary>
        public async Task<IEnumerable<AlarmaVacunacion>> GetVacunacionesProximasMesActualAsync()
        {
            return await _alarmaRepository.GetVacunacionesProximasMesActualAsync();
        }

        /// <summary>
        /// Marca una alarma como notificada (ya se notificó al paciente)
        /// </summary>
        public async Task<bool> MarcarComoNotificadaAsync(int alarmaId)
        {
            return await _alarmaRepository.MarcarComoNotificadaAsync(alarmaId);
        }

        /// <summary>
        /// Obtiene las alarmas vencidas validando por semanas
        /// </summary>
        public async Task<IEnumerable<AlarmaVacunacion>> GetAlarmasVencidasPorSemanasAsync()
        {
            return await _alarmaRepository.GetAlarmasVencidasPorSemanasAsync();
        }

        /// <summary>
        /// Crea alarmas automáticamente cuando se llama al endpoint de esquema vacunación
        /// </summary>
        public async Task CrearAlarmaDesdeEsquemaAsync(int pacienteId, int vacunaId, int numeroDosiActual, DateTime fechaUltimaAplicacion)
        {
            // Obtener información de la vacuna
            var vacuna = await _vacunaRepository.GetByIdAsync(vacunaId);
            if (vacuna == null || numeroDosiActual >= vacuna.NumeroDosis)
                return; // No crear alarma si es la última dosis o vacuna no existe

            // Verificar si ya existe una alarma pendiente
            if (await _alarmaRepository.ExisteAlarmaPendienteAsync(pacienteId, vacunaId))
                return;

            // Calcular la fecha de próxima aplicación usando IntervaloSemanas
            var fechaProximaAplicacion = fechaUltimaAplicacion.AddDays(vacuna.IntervaloSemanas * 7);

            // Crear la alarma
            var alarma = new AlarmaVacunacion
            {
                PacienteId = pacienteId,
                VacunaId = vacunaId,
                DosisActual = numeroDosiActual,
                FechaPrimeraAplicacion = fechaUltimaAplicacion, // Simplificado para este ejemplo
                FechaUltimaAplicacion = fechaUltimaAplicacion,
                FechaProximaAplicacion = fechaProximaAplicacion,
                EsquemaCompletado = false,
                NotificacionEnviada = false
            };

            await _alarmaRepository.AddAsync(alarma);
        }
    }
}
