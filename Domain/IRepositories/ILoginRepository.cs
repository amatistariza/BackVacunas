using API.Domain.Models;

namespace API.Domain.IRepositories;

public interface ILoginRepository
{
    Task<Usuario> ValidateUser(Usuario usuario);
}