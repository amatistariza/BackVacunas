using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlarmasVacunacionController : ControllerBase
    {
        private readonly IAlarmaVacunacionService _alarmaService;

        public AlarmasVacunacionController(IAlarmaVacunacionService alarmaService)
        {
            _alarmaService = alarmaService;
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<IActionResult> GetAlarmasByPaciente(int pacienteId)
        {
            try
            {
                var alarmas = await _alarmaService.GetAlarmasByPacienteAsync(pacienteId);
                return Ok(alarmas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al obtener las alarmas.", error = ex.Message });
            }
        }

        [HttpGet("pendientes")]
        public async Task<IActionResult> GetAlarmasPendientes()
        {
            try
            {
                var alarmas = await _alarmaService.GetAlarmasPendientesAsync();
                return Ok(alarmas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al obtener las alarmas pendientes.", error = ex.Message });
            }
        }

        [HttpGet("vacuna/{vacunaId}")]
        public async Task<IActionResult> GetAlarmasByVacuna(int vacunaId)
        {
            try
            {
                var alarmas = await _alarmaService.GetAlarmasByVacunaAsync(vacunaId);
                return Ok(alarmas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al obtener las alarmas por vacuna.", error = ex.Message });
            }
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearAlarma([FromBody] CrearAlarmaDTO dto)
        {
            try
            {
                await _alarmaService.CrearAlarmaAsync(dto.PacienteId, dto.VacunaId, dto.DosisActual, dto.FechaAplicacion);
                return Ok(new { mensaje = "Alarma creada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al crear la alarma.", error = ex.Message });
            }
        }

        [HttpPut("notificar/{alarmaId}")]
        public async Task<IActionResult> MarcarComoNotificada(int alarmaId)
        {
            try
            {
                await _alarmaService.MarcarComoNotificadaAsync(alarmaId);
                return Ok(new { mensaje = "Alarma marcada como notificada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al marcar la alarma como notificada.", error = ex.Message });
            }
        }

        [HttpPost("registrar-dosis")]
        public async Task<IActionResult> RegistrarSiguienteDosis([FromBody] RegistrarDosisDTO dto)
        {
            try
            {
                await _alarmaService.RegistrarSiguienteDosisAsync(dto.AlarmaId, dto.FechaAplicacion);
                return Ok(new { mensaje = "Siguiente dosis registrada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al registrar la siguiente dosis.", error = ex.Message });
            }
        }

        [HttpPut("completar/{alarmaId}")]
        public async Task<IActionResult> CompletarEsquema(int alarmaId)
        {
            try
            {
                await _alarmaService.CompletarEsquemaAsync(alarmaId);
                return Ok(new { mensaje = "Esquema de vacunación completado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al completar el esquema.", error = ex.Message });
            }
        }
    }

    public class CrearAlarmaDTO
    {
        public int PacienteId { get; set; }
        public int VacunaId { get; set; }
        public int DosisActual { get; set; }
        public DateTime FechaAplicacion { get; set; }
    }

    public class RegistrarDosisDTO
    {
        public int AlarmaId { get; set; }
        public DateTime FechaAplicacion { get; set; }
    }
}