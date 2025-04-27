using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MadresController : ControllerBase
{
    private readonly IMadreService _madreService;

    public MadresController(IMadreService madreService)
    {
        _madreService = madreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var madres = await _madreService.GetAllAsync();
        return Ok(madres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var madre = await _madreService.GetByIdAsync(id);
        if (madre == null)
            return NotFound($"No se encontró la madre con ID {id}");
        return Ok(madre);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Madre madre)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _madreService.AddAsync(madre);
        return CreatedAtAction(nameof(GetById), new { id = madre.Id }, madre);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Madre madre)
    {
        if (id != madre.Id)
            return BadRequest("El ID proporcionado no coincide con el ID de la entidad.");

        await _madreService.UpdateAsync(madre);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _madreService.DeleteAsync(id);
        return NoContent();
    }
}
