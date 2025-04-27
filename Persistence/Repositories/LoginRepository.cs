using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repositories;

public class LoginRepository : ILoginRepository
{

    private readonly AplicationDbContext _context;

    public LoginRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> ValidateUser(Usuario usuario)
    {
        var user = await _context.Usuarios.Where(x => x.NombreUsuario == usuario.NombreUsuario
                                            && x.Password == usuario.Password && x.RolUser == usuario.RolUser).FirstOrDefaultAsync();
        return user;
    }
}