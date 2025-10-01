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
    public async Task<IActionResult> Update(int id, [FromBody] PacienteEditDTO dto)
    {
        if (dto == null)
            return NoContent(); // no hay cambios

        // Cargar existente
        var existente = await _pacienteService.GetByIdAsync(id);
        if (existente == null)
            return NotFound(new { mensaje = "Paciente no encontrado." });

        // Aplicar cambios parciales solo si vienen en el payload
        bool cambios = false;
        void set<T>(T? val, Action<T> apply) where T : struct { if (val.HasValue) { apply(val.Value); cambios = true; } }
    void setRef(string val, Action<string> apply) { if (!string.IsNullOrWhiteSpace(val)) { apply(val); cambios = true; } }

        setRef(dto.TipoIdentificacion, v => existente.TipoIdentificacion = v.Trim().ToUpperInvariant());
        setRef(dto.NumeroIdentificacion, v => existente.NumeroIdentificacion = v.Trim());
        setRef(dto.PrimerNombre, v => existente.PrimerNombre = v);
        setRef(dto.SegundoNombre, v => existente.SegundoNombre = v);
        setRef(dto.PrimerApellido, v => existente.PrimerApellido = v);
        setRef(dto.SegundoApellido, v => existente.SegundoApellido = v);
        set(dto.FechaAtencion, v => existente.FechaAtencion = v.Date);
        set(dto.FechaNacimiento, v => existente.FechaNacimiento = v.Date);
        setRef(dto.Sexo, v => existente.Sexo = v);
        setRef(dto.OrientacionSexual, v => existente.OrientacionSexual = v);
        set(dto.EdadGestacionalSemanas, v => existente.EdadGestacionalSemanas = v);
        setRef(dto.PaisNacimiento, v => existente.PaisNacimiento = v);
        setRef(dto.EstatusMigratorio, v => existente.EstatusMigratorio = v);
        setRef(dto.RegimenAfiliacion, v => existente.RegimenAfiliacion = v);
        setRef(dto.Aseguradora, v => existente.Aseguradora = v);
        setRef(dto.PertenenciaEtnica, v => existente.PertenenciaEtnica = v);
        set(dto.Desplazado, v => existente.Desplazado = v);
        set(dto.Discapacitado, v => existente.Discapacitado = v);
        set(dto.Fallecido, v => existente.Fallecido = v);
        set(dto.VictimaConflictoArmado, v => existente.VictimaConflictoArmado = v);
        set(dto.EstudiaActualmente, v => existente.EstudiaActualmente = v);
        setRef(dto.PaisResidencia, v => existente.PaisResidencia = v);
        setRef(dto.DepartamentoResidencia, v => existente.DepartamentoResidencia = v);
        setRef(dto.MunicipioResidencia, v => existente.MunicipioResidencia = v);
        setRef(dto.ComunaLocalidad, v => existente.ComunaLocalidad = v);
        setRef(dto.Area, v => existente.Area = v);
        setRef(dto.Direccion, v => existente.Direccion = v);
        setRef(dto.TelefonoFijo, v => existente.TelefonoFijo = v);
        setRef(dto.Celular, v => existente.Celular = v);
        setRef(dto.Email, v => existente.Email = v);
        set(dto.AutorizaLlamadasTelefonicas, v => existente.AutorizaLlamadasTelefonicas = v);
        set(dto.AutorizaEnvioCorreo, v => existente.AutorizaEnvioCorreo = v);
        set(dto.MadreId, v => existente.MadreId = v);
        set(dto.CuidadorId, v => existente.CuidadorId = v);

        if (cambios)
        {
            await _pacienteService.UpdateAsync(existente);
            return Ok(new { mensaje = "Paciente actualizado correctamente.", status = 200 });
        }
        return Ok(new { mensaje = "Sin cambios efectivos.", status = 200 });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _pacienteService.DeleteAsync(id);
        return NoContent();
    }
}
