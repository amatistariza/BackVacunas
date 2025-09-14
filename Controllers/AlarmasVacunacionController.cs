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

        /// <summary>
        /// Obtiene las vacunaciones próximas del mes actual
        /// </summary>
        [HttpGet("proximas-mes-actual")]
        public async Task<IActionResult> GetVacunacionesProximasMesActual()
        {
            try
            {
                var alarmas = await _alarmaService.GetVacunacionesProximasMesActualAsync();

                var data = alarmas.Select(a => new API.DTO.AlarmaProximaDto
                {
                    Id = a.Id,
                    TipoIdentificacion = a.Paciente?.TipoIdentificacion ?? string.Empty,
                    NumeroIdentificacion = a.Paciente?.NumeroIdentificacion ?? string.Empty,
                    Nombre = string.Join(" ", new[] { a.Paciente?.PrimerNombre, a.Paciente?.SegundoNombre, a.Paciente?.PrimerApellido, a.Paciente?.SegundoApellido }.Where(s => !string.IsNullOrWhiteSpace(s))),
                    Telefono = a.Paciente?.TelefonoFijo,
                    Celular = a.Paciente?.Celular,
                    Correo = a.Paciente?.Email,
                    Pendiente = $"Dosis {a.DosisActual + 1} de {a.Vacuna?.Nombre}",
                    FechaAplicacion = a.FechaProximaAplicacion,
                    Ok = a.NotificacionEnviada
                }).ToList();

                return Ok(new {
                    mensaje = "Vacunaciones próximas del mes actual obtenidas exitosamente.",
                    data,
                    total = data.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    mensaje = "Ocurrió un error al obtener las vacunaciones próximas.", 
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        /// <summary>
        /// Marca una alarma como notificada (ya se notificó al paciente)
        /// </summary>
        [HttpPut("{alarmaId}/marcar-notificada")]
        public async Task<IActionResult> MarcarAlarmaComoNotificada(int alarmaId)
        {
            try
            {
                var resultado = await _alarmaService.MarcarComoNotificadaAsync(alarmaId);
                if (resultado)
                {
                    return Ok(new { 
                        mensaje = "Alarma marcada como notificada exitosamente.",
                        alarmaId = alarmaId
                    });
                }
                else
                {
                    return NotFound(new { 
                        mensaje = "No se encontró la alarma especificada.",
                        alarmaId = alarmaId
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    mensaje = "Ocurrió un error al marcar la alarma como notificada.", 
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        /// <summary>
        /// Obtiene las alarmas vencidas (validación por semanas)
        /// </summary>
        [HttpGet("vencidas")]
        public async Task<IActionResult> GetAlarmasVencidas()
        {
            try
            {
                var alarmas = await _alarmaService.GetAlarmasVencidasPorSemanasAsync();
                return Ok(new { 
                    mensaje = "Alarmas vencidas obtenidas exitosamente.",
                    data = alarmas,
                    total = alarmas.Count()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    mensaje = "Ocurrió un error al obtener las alarmas vencidas.", 
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}