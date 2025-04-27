using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AntecedentesMedicosController : ControllerBase
{
    private readonly IAntecedentesMedicosService _antecedentesMedicosService;

    public AntecedentesMedicosController(IAntecedentesMedicosService antecedentesMedicosService)
    {
        _antecedentesMedicosService = antecedentesMedicosService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var antecedentes = await _antecedentesMedicosService.GetAllAsync();
        return Ok(antecedentes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var antecedente = await _antecedentesMedicosService.GetByIdAsync(id);
        if (antecedente == null)
            return NotFound($"No se encontró el antecedente médico con ID {id}");
        return Ok(antecedente);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AntecedentesMedicos antecedentesMedicos)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _antecedentesMedicosService.AddAsync(antecedentesMedicos);
        return CreatedAtAction(nameof(GetById), new { id = antecedentesMedicos.Id }, antecedentesMedicos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AntecedentesMedicos antecedentesMedicos)
    {
        if (id != antecedentesMedicos.Id)
            return BadRequest("El ID proporcionado no coincide con el ID de la entidad.");

        await _antecedentesMedicosService.UpdateAsync(antecedentesMedicos);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _antecedentesMedicosService.DeleteAsync(id);
        return NoContent();
    }
}
