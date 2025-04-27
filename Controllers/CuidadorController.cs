using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuidadorController : ControllerBase
{
    private readonly ICuidadorService _cuidadorService;

    public CuidadorController(ICuidadorService cuidadorService)
    {
        _cuidadorService = cuidadorService;
    }

    // Obtener todos los cuidadores
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cuidadores = await _cuidadorService.GetAllAsync();
        return Ok(cuidadores);
    }

    // Obtener un cuidador por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cuidador = await _cuidadorService.GetByIdAsync(id);
        if (cuidador == null)
            return NotFound($"No se encontró el cuidador con ID {id}");
        return Ok(cuidador);
    }

    // Crear un nuevo cuidador
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Cuidador cuidador)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _cuidadorService.AddAsync(cuidador);
        return CreatedAtAction(nameof(GetById), new { id = cuidador.Id }, cuidador);
    }

    // Actualizar un cuidador existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Cuidador cuidador)
    {
        if (id != cuidador.Id)
            return BadRequest("El ID proporcionado no coincide con el ID de la entidad.");

        await _cuidadorService.UpdateAsync(cuidador);
        return NoContent();
    }

    // Eliminar un cuidador por su ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cuidadorService.DeleteAsync(id);
        return NoContent();
    }
}