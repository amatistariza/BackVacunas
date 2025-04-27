using API.Domain.IServices;
using API.Domain.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacienteController : ControllerBase
{
    private readonly IPacienteService _pacienteService;

    public PacienteController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pacientes = await _pacienteService.GetAllAsync();
        return Ok(pacientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);
        if (paciente == null)
            return NotFound(new { mensaje = "Paciente no encontrado." });

        return Ok(paciente);
    }

    // Obtener antecedentes por ID de paciente
    [HttpGet("BuscarPaciente/{numeroIdentificacion}")]
    public async Task<IActionResult> GetByPacienteId(string numeroIdentificacion)
    {
        var paciente = await _pacienteService.GetByPacienteIdAsync(numeroIdentificacion);
        return Ok(paciente);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Paciente paciente)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _pacienteService.AddAsync(paciente);
        return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Paciente paciente)
    {
        if (id != paciente.Id)
            return BadRequest(new { mensaje = "El ID proporcionado no coincide con el ID del paciente." });

        await _pacienteService.UpdateAsync(paciente);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _pacienteService.DeleteAsync(id);
        return NoContent();
    }
}
