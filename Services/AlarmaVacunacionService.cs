using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AlarmaVacunacionService : IAlarmaVacunacionService
    {
        private readonly IAlarmaVacunacionRepository _alarmaRepository;
        private readonly IVacunaRepository _vacunaRepository;
        private readonly IEsquemaVacunacionRepository _esquemaRepository;

        public AlarmaVacunacionService(
            IAlarmaVacunacionRepository alarmaRepository,
            IVacunaRepository vacunaRepository,
            IEsquemaVacunacionRepository esquemaRepository)
        {
            _alarmaRepository = alarmaRepository;
            _vacunaRepository = vacunaRepository;
            _esquemaRepository = esquemaRepository;
        }

        // Sobrecarga para compatibilidad con pruebas existentes
        public AlarmaVacunacionService(
            IAlarmaVacunacionRepository alarmaRepository,
            IVacunaRepository vacunaRepository)
            : this(alarmaRepository, vacunaRepository, null!)
        { }

        /// <summary>
        /// Obtiene las vacunaciones próximas del mes actual
        /// </summary>
        public async Task<IEnumerable<AlarmaVacunacion>> GetVacunacionesProximasMesActualAsync()
        {
            // Reconciliar: asegurar que existan alarmas para esquemas con próxima dosis en el mes actual
            var hoy = DateTime.Today;
            var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
            var inicioMesSiguiente = inicioMes.AddMonths(1);

            // Acceder al DbContext para consultar Esquemas sin ampliar interfaz
            var contextField = _esquemaRepository.GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var dbContext = contextField?.GetValue(_esquemaRepository) as API.Persistence.Context.AplicationDbContext;
            if (dbContext != null)
            {
                var esquemasMes = dbContext.EsquemasVacunacion
                    .AsNoTracking()
                    .Where(e => e.FechaProximaDosis.HasValue &&
                                e.FechaProximaDosis.Value >= inicioMes &&
                                e.FechaProximaDosis.Value < inicioMesSiguiente)
                    .Select(e => new { e.PacienteId, e.VacunaId, e.NumeroDeDosis, e.FechaDosisAplicada, e.FechaProximaDosis })
                    .ToList();

                foreach (var e in esquemasMes)
                {
                    // upsert de alarma por cada esquema relevante
                    await CrearAlarmaDesdeEsquemaAsync(e.PacienteId, e.VacunaId, e.NumeroDeDosis, e.FechaDosisAplicada, e.FechaProximaDosis);
                }
            }

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
    public async Task CrearAlarmaDesdeEsquemaAsync(int pacienteId, int vacunaId, int numeroDosiActual, DateTime fechaUltimaAplicacion, DateTime? fechaProximaAplicacion = null)
        {
            // Obtener información de la vacuna
            var vacuna = await _vacunaRepository.GetByIdAsync(vacunaId);
            if (vacuna == null || numeroDosiActual >= vacuna.NumeroDosis)
                return; // No crear alarma si es la última dosis o vacuna no existe

            // Calcular la fecha de próxima aplicación usando IntervaloSemanas (solo fecha) o usar la provista
            var proxima = (fechaProximaAplicacion?.Date) ?? fechaUltimaAplicacion.Date.AddDays(vacuna.IntervaloSemanas * 7).Date;

            // Si ya existe una alarma pendiente, actualizarla; si no, crearla
            var existente = await _alarmaRepository.GetPendienteAsync(pacienteId, vacunaId);
            if (existente != null)
            {
                // Mantener la primera aplicación original si ya existía
                existente.DosisActual = numeroDosiActual;
                existente.FechaUltimaAplicacion = fechaUltimaAplicacion.Date;
                // Si la fecha próxima cambió, se reinicia la notificación; si no, se conserva el estado 'ok'
                bool cambioProxima = existente.FechaProximaAplicacion.Date != proxima;
                existente.FechaProximaAplicacion = proxima;
                if (cambioProxima)
                {
                    existente.NotificacionEnviada = false;
                    existente.FechaNotificacion = null;
                }
                await _alarmaRepository.UpdateAsync(existente);
            }
            else
            {
                var alarma = new AlarmaVacunacion
                {
                    PacienteId = pacienteId,
                    VacunaId = vacunaId,
                    DosisActual = numeroDosiActual,
                    FechaPrimeraAplicacion = fechaUltimaAplicacion.Date, // primera aplicación
                    FechaUltimaAplicacion = fechaUltimaAplicacion.Date,
                    FechaProximaAplicacion = proxima,
                    EsquemaCompletado = false,
                    NotificacionEnviada = false
                };

                await _alarmaRepository.AddAsync(alarma);
            }
        }

        public async Task<bool> MarcarEsquemaCompletadoAsync(int pacienteId, int vacunaId)
        {
            var pendiente = await _alarmaRepository.GetPendienteAsync(pacienteId, vacunaId);
            if (pendiente == null) return false;
            pendiente.EsquemaCompletado = true;
            pendiente.NotificacionEnviada = true; // opcional: ya no notificar más
            await _alarmaRepository.UpdateAsync(pendiente);
            return true;
        }
    }
}
