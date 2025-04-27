using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;
using API.Domain.Models.Esquema;

namespace API.Services
{
    public class AlarmaVacunacionService : IAlarmaVacunacionService
    {
        private readonly IAlarmaVacunacionRepository _alarmaRepository;
        private readonly IVacunaRepository _vacunaRepository;
        private readonly IRepository<Paciente> _pacienteRepository;

        public AlarmaVacunacionService(
            IAlarmaVacunacionRepository alarmaRepository,
            IVacunaRepository vacunaRepository,
            IRepository<Paciente> pacienteRepository)
        {
            _alarmaRepository = alarmaRepository;
            _vacunaRepository = vacunaRepository;
            _pacienteRepository = pacienteRepository;
        }

        public async Task CrearAlarmaAsync(int pacienteId, int vacunaId, int dosisActual, DateTime fechaAplicacion)
        {
            // Obtener la vacuna para verificar el número total de dosis y el intervalo
            var vacuna = await _vacunaRepository.GetByIdAsync(vacunaId);
            if (vacuna == null)
            {
                throw new InvalidOperationException("La vacuna especificada no existe.");
            }

            // Verificar si es la última dosis
            bool esEsquemaCompleto = dosisActual >= vacuna.NumeroDosis;

            // Si no es la última dosis, calcular la fecha de la próxima aplicación
            DateTime fechaProximaAplicacion = fechaAplicacion.AddMonths(vacuna.IntervaloMeses);

            // Crear la alarma
            var alarma = new AlarmaVacunacion
            {
                PacienteId = pacienteId,
                VacunaId = vacunaId,
                DosisActual = dosisActual,
                FechaPrimeraAplicacion = dosisActual == 1 ? fechaAplicacion : DateTime.MinValue, // Se establecerá después si no es primera dosis
                FechaUltimaAplicacion = fechaAplicacion,
                FechaProximaAplicacion = fechaProximaAplicacion,
                EsquemaCompletado = esEsquemaCompleto,
                NotificacionEnviada = false
            };

            // Si no es la primera dosis, buscar la alarma anterior para obtener la fecha de primera aplicación
            if (dosisActual > 1)
            {
                var alarmasAnteriores = await _alarmaRepository.GetAlarmasByPacienteAsync(pacienteId);
                var alarmaAnterior = alarmasAnteriores.FirstOrDefault(a => a.VacunaId == vacunaId);
                
                if (alarmaAnterior != null)
                {
                    // Usar la fecha de primera aplicación de la alarma anterior
                    alarma.FechaPrimeraAplicacion = alarmaAnterior.FechaPrimeraAplicacion;
                    
                    // Marcar la alarma anterior como completada
                    await _alarmaRepository.CompletarEsquemaAsync(alarmaAnterior.Id);
                }
            }

            // Guardar la alarma
            await _alarmaRepository.AddAsync(alarma);
        }

        public async Task<IEnumerable<AlarmaVacunacion>> GetAlarmasByPacienteAsync(int pacienteId)
        {
            return await _alarmaRepository.GetAlarmasByPacienteAsync(pacienteId);
        }

        public async Task<IEnumerable<AlarmaVacunacion>> GetAlarmasPendientesAsync()
        {
            return await _alarmaRepository.GetAlarmasPendientesAsync();
        }

        public async Task<IEnumerable<AlarmaVacunacion>> GetAlarmasByVacunaAsync(int vacunaId)
        {
            return await _alarmaRepository.GetAlarmasByVacunaAsync(vacunaId);
        }

        public async Task MarcarComoNotificadaAsync(int alarmaId)
        {
            await _alarmaRepository.MarcarComoNotificadaAsync(alarmaId);
        }

        public async Task RegistrarSiguienteDosisAsync(int alarmaId, DateTime fechaAplicacion)
        {
            // Obtener la alarma actual
            var alarma = await _alarmaRepository.GetByIdAsync(alarmaId);
            if (alarma == null)
            {
                throw new InvalidOperationException("La alarma especificada no existe.");
            }

            // Obtener la vacuna para verificar el número total de dosis y el intervalo
            var vacuna = await _vacunaRepository.GetByIdAsync(alarma.VacunaId);
            if (vacuna == null)
            {
                throw new InvalidOperationException("La vacuna especificada no existe.");
            }

            // Registrar la aplicación de la siguiente dosis
            int nuevaDosis = alarma.DosisActual + 1;

            // Crear una nueva alarma para la siguiente dosis
            await CrearAlarmaAsync(alarma.PacienteId, alarma.VacunaId, nuevaDosis, fechaAplicacion);

            // Marcar la alarma actual como completada
            await CompletarEsquemaAsync(alarmaId);
        }

        public async Task CompletarEsquemaAsync(int alarmaId)
        {
            await _alarmaRepository.CompletarEsquemaAsync(alarmaId);
        }
    }
}