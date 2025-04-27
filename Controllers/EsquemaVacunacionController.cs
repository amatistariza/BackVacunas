using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EsquemaVacunacionController : ControllerBase
{
    private readonly IEsquemaVacunacionService _esquemaService;

    public EsquemaVacunacionController(IEsquemaVacunacionService esquemaService)
    {
        _esquemaService = esquemaService;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarEsquema([FromBody] EsquemaVacunacion esquemaVacunacion)
    {
        try
        {
            await _esquemaService.RegistrarEsquemaAsync(esquemaVacunacion);
            return Ok("Esquema registrado correctamente.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", error = ex.Message });
        }
    }

    [HttpGet("{esquemaId}")]
    public async Task<IActionResult> GetEsquemaConDetalles(int esquemaId)
    {
        try
        {
            var esquema = await _esquemaService.GetEsquemaConDetallesAsync(esquemaId);
            if (esquema == null)
                return NotFound(new { mensaje = "Esquema no encontrado." });

            return Ok(esquema);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", error = ex.Message });
        }
    }

}
