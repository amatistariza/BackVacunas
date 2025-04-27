using API.Domain.IServices;
using API.Domain.Models.Esquema;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JeringasController : ControllerBase
{
    private readonly IJeringaService _jeringaService;

    public JeringasController(IJeringaService jeringaService)
    {
        _jeringaService = jeringaService;
    }

    // Obtener todas las jeringas
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var jeringas = await _jeringaService.GetAllAsync();
        return Ok(jeringas);
    }

    // Obtener una jeringa por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var jeringa = await _jeringaService.GetByIdAsync(id);
        if (jeringa == null)
            return NotFound($"No se encontró la jeringa con ID {id}");
        return Ok(jeringa);
    }

    // Crear una nueva jeringa
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Jeringa jeringa)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _jeringaService.AddAsync(jeringa);
        return CreatedAtAction(nameof(GetById), new { id = jeringa.Id }, jeringa);
    }

    // Actualizar una jeringa existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Jeringa jeringa)
    {
        if (id != jeringa.Id)
            return BadRequest("El ID proporcionado no coincide con el ID de la entidad.");

        await _jeringaService.UpdateAsync(jeringa);
        return NoContent();
    }

    // Eliminar una jeringa por su ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _jeringaService.DeleteAsync(id);
        return NoContent();
    }
}