using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AplicationDbContext _context;

    public UsuarioRepository(AplicationDbContext context)
    {
        _context = context;
    }
    public async Task UpdateRol(Usuario usuario)
    {
        _context.Update(usuario);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Usuario>> GetListUsuarios()
    {
        var listUsuarios = await _context.Usuarios.Where(x => x.Id != null).ToListAsync();
        return listUsuarios;
    }

    public async Task<Usuario> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
        return usuario;
    }

    public async Task SavedUser(Usuario usuario)
    {
        _context.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePassword(Usuario usuario)
    {
        _context.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ValidateExistence(Usuario usuario)
    {
        var validateExistence = await _context.Usuarios.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario);
        return validateExistence;
    }

    public async Task<Usuario> ValidatePassword(int idUsuario, string passwordAnterior)
    {
        var usuario = await _context.Usuarios.Where(x => x.Id == idUsuario && x.Password == passwordAnterior).FirstOrDefaultAsync();
        return usuario;
    }
    
    public async Task DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
