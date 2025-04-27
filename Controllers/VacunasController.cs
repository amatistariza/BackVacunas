using API.Domain.IServices;
using API.Domain.Models.Esquema;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMINISTRADOR")]
public class VacunasController : ControllerBase
{
    private readonly IVacunaService _vacunaService;

    public VacunasController(IVacunaService vacunaService)
    {
        _vacunaService = vacunaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vacunas = await _vacunaService.GetAllAsync();
        return Ok(vacunas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vacuna = await _vacunaService.GetByIdAsync(id);
        if (vacuna == null) return NotFound($"No se encontró la vacuna con ID {id}");
        return Ok(vacuna);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Vacuna vacuna)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _vacunaService.AddAsync(vacuna);
        return CreatedAtAction(nameof(GetById), new { id = vacuna.Id }, vacuna);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Vacuna vacuna)
    {
        if (id != vacuna.Id) return BadRequest("El ID no coincide.");
        await _vacunaService.UpdateAsync(vacuna);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _vacunaService.DeleteAsync(id);
        return NoContent();
    }
}