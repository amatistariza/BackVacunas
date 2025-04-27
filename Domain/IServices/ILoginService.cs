using API.Domain.Models;

public interface ILoginService
{
    Task<Usuario> ValidateUser(Usuario usuario);
}