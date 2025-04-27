using API.Domain.IServices;
using API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CondicionUsuariaController : ControllerBase
{
    private readonly ICondicionUsuariaService _condicionUsuariaService;

    public CondicionUsuariaController(ICondicionUsuariaService condicionUsuariaService)
    {
        _condicionUsuariaService = condicionUsuariaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var condiciones = await _condicionUsuariaService.GetAllAsync();
        return Ok(condiciones);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var condicion = await _condicionUsuariaService.GetByIdAsync(id);
        if (condicion == null)
            return NotFound($"No se encontró la condición con ID {id}");
        return Ok(condicion);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CondicionUsuaria condicionUsuaria)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _condicionUsuariaService.AddAsync(condicionUsuaria);
        return CreatedAtAction(nameof(GetById), new { id = condicionUsuaria.Id }, condicionUsuaria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CondicionUsuaria condicionUsuaria)
    {
        if (id != condicionUsuaria.Id)
            return BadRequest("El ID proporcionado no coincide con el ID de la entidad.");

        await _condicionUsuariaService.UpdateAsync(condicionUsuaria);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _condicionUsuariaService.DeleteAsync(id);
        return NoContent();
    }
}