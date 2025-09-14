using API.Domain.IServices;
using API.Domain.Models;
using API.Services;
using API.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacienteController : ControllerBase
{
    private readonly IPacienteService _pacienteService;
    private readonly IMapper _mapper;

    public PacienteController(IPacienteService pacienteService, IMapper mapper)
    {
        _pacienteService = pacienteService;
        _mapper = mapper;
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
    public async Task<IActionResult> Add([FromBody] PacienteCreateDTO pacienteCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Validación: no permitir registrar pacientes ya registrados por NumeroIdentificacion
        // Nota: se asume unicidad por NumeroIdentificacion; si también debe incluir TipoIdentificacion, ampliamos el repositorio.
        var existente = await _pacienteService.GetByPacienteIdAsync(pacienteCreateDto.NumeroIdentificacion);
        if (existente != null)
            return Conflict(new { mensaje = "Paciente ya registrado con ese número de identificación." });

        // Map DTO to Entity
        var paciente = _mapper.Map<Paciente>(pacienteCreateDto);
        
        await _pacienteService.AddAsync(paciente);
        
        // Return the created entity as response DTO
        var createdPaciente = await _pacienteService.GetByIdAsync(paciente.Id);
        return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, createdPaciente);
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
