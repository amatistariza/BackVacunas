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
    public async Task<IActionResult> Update(int id, [FromBody] PacienteUpdateDTO dto)
    {
        if (dto == null)
            return NoContent(); // no hay cambios

        // Cargar existente
        var existente = await _pacienteService.GetByIdAsync(id);
        if (existente == null)
            return NotFound(new { mensaje = "Paciente no encontrado." });

        // Aplicar cambios parciales solo si vienen en el payload
        bool cambios = false;
        if (!string.IsNullOrWhiteSpace(dto.Aseguradora) && dto.Aseguradora != existente.Aseguradora)
        {
            existente.Aseguradora = dto.Aseguradora;
            cambios = true;
        }
        if (!string.IsNullOrWhiteSpace(dto.RegimenAfiliacion) && dto.RegimenAfiliacion != existente.RegimenAfiliacion)
        {
            existente.RegimenAfiliacion = dto.RegimenAfiliacion;
            cambios = true;
        }
        if (!string.IsNullOrWhiteSpace(dto.PertenenciaEtnica) && dto.PertenenciaEtnica != existente.PertenenciaEtnica)
        {
            existente.PertenenciaEtnica = dto.PertenenciaEtnica;
            cambios = true;
        }

        if (cambios)
        {
            await _pacienteService.UpdateAsync(existente);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _pacienteService.DeleteAsync(id);
        return NoContent();
    }
}
