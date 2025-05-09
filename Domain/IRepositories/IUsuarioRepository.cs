﻿using API.Domain.Models;
using API.DTO;

namespace API.Domain.IRepositories;

public interface IUsuarioRepository
{
    Task SavedUser(Usuario usuario);

    Task<bool> ValidateExistence(Usuario usuario);

    Task<Usuario> ValidatePassword(int idUsuario, string passwordAnterior);

    Task UpdatePassword(Usuario usuario);

    Task<List<Usuario>> GetListUsuarios();

    Task<Usuario> GetUsuario(int id);

    Task UpdateRol(Usuario usuario);
    
    Task DeleteAsync(int id);
    
    Task<Usuario> UpdateUsuarioAsync(Usuario usuario);
}
