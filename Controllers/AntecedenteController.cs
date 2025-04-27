using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AntecedenteController : ControllerBase
{
    private readonly IAntecedenteService _antecedenteService;

    public AntecedenteController(IAntecedenteService antecedenteService)
    {
        _antecedenteService = antecedenteService;
    }

    // Obtener todos los antecedentes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var antecedentes = await _antecedenteService.GetAllAsync();
        return Ok(antecedentes);
    }

    // Obtener antecedentes por ID de paciente
    [HttpGet("paciente/{pacienteId}")]
    public async Task<IActionResult> GetByPacienteId(int pacienteId)
    {
        var antecedentes = await _antecedenteService.GetAllAsync();
        var result = antecedentes.Where(a => a.PacienteId == pacienteId);
        return Ok(result);
    }

    // Obtener un antecedente por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var antecedente = await _antecedenteService.GetByIdAsync(id);
        if (antecedente == null)
            return NotFound($"No se encontró el antecedente con ID {id}");
        return Ok(antecedente);
    }

    // Crear un nuevo antecedente
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Antecedente antecedente)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _antecedenteService.AddAsync(antecedente);
        return CreatedAtAction(nameof(GetById), new { id = antecedente.Id }, antecedente);
    }

    // Actualizar un antecedente existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Antecedente antecedente)
    {
        if (id != antecedente.Id)
            return BadRequest("El ID proporcionado no coincide con el ID de la entidad.");

        await _antecedenteService.UpdateAsync(antecedente);
        return NoContent();
    }

    // Eliminar un antecedente por su ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _antecedenteService.DeleteAsync(id);
        return NoContent();
    }
}