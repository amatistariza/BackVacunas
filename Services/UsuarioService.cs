using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;
using API.DTO;
using API.Utils;

namespace API.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<List<Usuario>> GetListUsuarios()
    {
        return await _usuarioRepository.GetListUsuarios();
    }

    public async Task<Usuario> GetUsuario(int id)
    {
        return await _usuarioRepository.GetUsuario(id);
    }
    public async Task UpdateRol(Usuario usuario)
    {
        await _usuarioRepository.UpdateRol(usuario);
    }

    public async Task SavedUser(Usuario usuario)
    {
        await _usuarioRepository.SavedUser(usuario);
    }

    public async Task UpdatePassword(Usuario usuario)
    {
        await _usuarioRepository.UpdatePassword(usuario);
    }

    public async Task<bool> ValidateExistence(Usuario usuario)
    {
        return await _usuarioRepository.ValidateExistence(usuario);
    }

    public async Task<Usuario> ValidatePassword(int idUsuario, string passwordAnterior)
    {
        return await _usuarioRepository.ValidatePassword(idUsuario, passwordAnterior);
    }
    
    public async Task DeleteAsync(int id)
    {
        await _usuarioRepository.DeleteAsync(id);
    }
    
    public async Task UpdatePasswordById(int id, string newPassword)
    {
        var usuario = await _usuarioRepository.GetUsuario(id);
        if (usuario != null)
        {
            usuario.Password = newPassword;
            await _usuarioRepository.UpdatePassword(usuario);
        }
    }

    public async Task<Usuario> UpdateUsuarioAsync(int id, ActualizarUsuarioDTO actualizarUsuarioDTO)
    {
        var usuario = await _usuarioRepository.GetUsuario(id);
        if (usuario == null)
            return null;
        
        // Actualizar solo los campos proporcionados
        if (!string.IsNullOrEmpty(actualizarUsuarioDTO.NombreUsuario))
        {
            usuario.NombreUsuario = actualizarUsuarioDTO.NombreUsuario;
        }
        
        if (!string.IsNullOrEmpty(actualizarUsuarioDTO.Password))
        {
            usuario.Password = Encriptar.EncriptarPassword(actualizarUsuarioDTO.Password);
        }
        
        return await _usuarioRepository.UpdateUsuarioAsync(usuario);
    }
}