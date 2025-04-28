using API.Domain.IServices;
using API.Domain.Models;
using API.DTO;
using API.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Usuario usuario)
    {
        try
        {
            var validateExistence = await _usuarioService.ValidateExistence(usuario);
            if (validateExistence)
            {
                return BadRequest(new { message = "El usuario " + usuario.NombreUsuario + " ya existe! " });
            }
            usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
            await _usuarioService.SavedUser(usuario);
            return Ok(new { message = "Usuario registrado con exito" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Route("GetListUsuarios")]
    [HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetListUsuarios()
    {
        try
        {
            var usuarios = await _usuarioService.GetListUsuarios();
            
            // Crear una nueva lista sin el campo password
            var usuariosSinPassword = usuarios.Select(u => new
            {
                u.Id,
                u.NombreUsuario,
                u.RolUser
            });
            
            return Ok(usuariosSinPassword);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var usuario = await _usuarioService.GetUsuario(id);
            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado" });
                
            // Crear un objeto anónimo sin el campo password
            var usuarioSinPassword = new
            {
                usuario.Id,
                usuario.NombreUsuario,
                usuario.RolUser
            };
            
            return Ok(usuarioSinPassword);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] ActualizarUsuarioDTO actualizarUsuarioDTO)
    {
        try
        {
            // Verificar que el usuario existe
            var usuarioExistente = await _usuarioService.GetUsuario(id);
            if (usuarioExistente == null)
                return NotFound(new { message = "Usuario no encontrado" });
            
            // Actualizar el usuario con los campos proporcionados
            var usuarioActualizado = await _usuarioService.UpdateUsuarioAsync(id, actualizarUsuarioDTO);
            
            // Devolver respuesta sin incluir el campo password
            var respuesta = new
            {
                message = "Usuario actualizado con éxito",
                usuario = new
                {
                    usuarioActualizado.Id,
                    usuarioActualizado.NombreUsuario,
                    usuarioActualizado.RolUser
                }
            };
            
            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // Nuevo endpoint para eliminar usuario por ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            // Verificar que el usuario existe
            var usuario = await _usuarioService.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound(new { message = "El usuario no existe" });
            }
            
            await _usuarioService.DeleteAsync(id);
            return Ok(new { message = "Usuario eliminado con éxito" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}