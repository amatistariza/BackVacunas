using API.Domain.IServices;
using API.Domain.Models.Esquema;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuerosController : ControllerBase
{
    private readonly ISueroService _sueroService;

    public SuerosController(ISueroService sueroService)
    {
        _sueroService = sueroService;
    }

    // Obtener todos los sueros
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sueros = await _sueroService.GetAllAsync();
        return Ok(sueros);
    }

    // Obtener un suero por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var suero = await _sueroService.GetByIdAsync(id);
        if (suero == null)
            return NotFound($"No se encontró el suero con ID {id}");
        return Ok(suero);
    }

    // Crear un nuevo suero
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Suero suero)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _sueroService.AddAsync(suero);
        return CreatedAtAction(nameof(GetById), new { id = suero.Id }, suero);
    }

    // Actualizar un suero existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Suero suero)
    {
        if (id != suero.Id)
            return BadRequest("El ID proporcionado no coincide con el ID de la entidad.");

        await _sueroService.UpdateAsync(suero);
        return NoContent();
    }

    // Eliminar un suero por su ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sueroService.DeleteAsync(id);
        return NoContent();
    }
}
