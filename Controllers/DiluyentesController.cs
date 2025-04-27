using API.Domain.IServices;
using API.Domain.Models.Esquema;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiluyentesController : ControllerBase
{
    private readonly IDiluyenteService _diluyenteService;

    public DiluyentesController(IDiluyenteService diluyenteService)
    {
        _diluyenteService = diluyenteService;
    }

    // Obtener todos los diluyentes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var diluyentes = await _diluyenteService.GetAllAsync();
        return Ok(diluyentes);
    }

    // Obtener un diluyente por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var diluyente = await _diluyenteService.GetByIdAsync(id);
        if (diluyente == null)
            return NotFound($"No se encontró el diluyente con ID {id}");
        return Ok(diluyente);
    }

    // Crear un nuevo diluyente
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Diluyente diluyente)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _diluyenteService.AddAsync(diluyente);
        return CreatedAtAction(nameof(GetById), new { id = diluyente.Id }, diluyente);
    }

    // Actualizar un diluyente existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Diluyente diluyente)
    {
        if (id != diluyente.Id)
            return BadRequest("El ID proporcionado no coincide con el ID de la entidad.");

        await _diluyenteService.UpdateAsync(diluyente);
        return NoContent();
    }

    // Eliminar un diluyente por su ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _diluyenteService.DeleteAsync(id);
        return NoContent();
    }
}