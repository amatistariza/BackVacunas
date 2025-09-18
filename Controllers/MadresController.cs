using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MadresController : ControllerBase
{
    private readonly IMadreService _madreService;

    public MadresController(IMadreService madreService)
    {
        _madreService = madreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var madres = await _madreService.GetAllAsync();
        return Ok(madres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var madre = await _madreService.GetByIdAsync(id);
            if (madre == null)
                return NotFound(new { mensaje = $"No se encontró la madre con ID {id}", status = 404 });
            return Ok(madre);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error interno del servidor: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Madre madre)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "Datos inválidos.", status = 400 });

            madre.TipoIdentificacion = (madre.TipoIdentificacion ?? string.Empty).Trim().ToUpperInvariant();
            madre.NumeroIdentificacion = (madre.NumeroIdentificacion ?? string.Empty).Trim();

            var existente = await _madreService.GetByIdentificacionAsync(madre.TipoIdentificacion, madre.NumeroIdentificacion);
            if (existente != null)
                return BadRequest(new { mensaje = "Ya existe una madre registrada con ese tipo y número de identificación.", status = 400 });

            await _madreService.AddAsync(madre);
            return Ok(new { mensaje = "Madre registrada correctamente.", status = 200 });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error interno del servidor: {ex.Message}", status = 500 });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Madre madre)
    {
        try
        {
            if (id != madre.Id)
                return BadRequest(new { mensaje = "El ID proporcionado no coincide con el ID de la entidad.", status = 400 });

            madre.TipoIdentificacion = (madre.TipoIdentificacion ?? string.Empty).Trim().ToUpperInvariant();
            madre.NumeroIdentificacion = (madre.NumeroIdentificacion ?? string.Empty).Trim();

            var existente = await _madreService.GetByIdentificacionAsync(madre.TipoIdentificacion, madre.NumeroIdentificacion);
            if (existente != null && existente.Id != id)
                return BadRequest(new { mensaje = "Ya existe otra madre registrada con ese tipo y número de identificación.", status = 400 });

            await _madreService.UpdateAsync(madre);
            return Ok(new { mensaje = "Madre actualizada correctamente.", status = 200 });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error interno del servidor: {ex.Message}", status = 500 });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _madreService.DeleteAsync(id);
            return Ok(new { mensaje = "Madre eliminada correctamente.", status = 200 });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error interno del servidor: {ex.Message}", status = 500 });
        }
    }
}
