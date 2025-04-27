using API.Domain.Models;

namespace API.Domain.IServices;

public interface IUsuarioService
{
    Task SavedUser(Usuario usuario);
    Task<bool> ValidateExistence(Usuario usuario);
    Task<Usuario> ValidatePassword(int idUsuario, string passwordAnterior);
    Task UpdatePassword(Usuario usuario);
    Task<List<Usuario>> GetListUsuarios();
    Task<Usuario> GetUsuario(int id);
    Task UpdateRol(Usuario usuario);
    Task DeleteAsync(int id);
    Task UpdatePasswordById(int id, string newPassword);
}