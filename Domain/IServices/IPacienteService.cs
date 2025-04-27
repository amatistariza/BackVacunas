using API.Domain.DTOs;
using API.Domain.Models;

namespace API.Domain.IServices;

public interface IPacienteService : IBaseService<Paciente>
{
    // Métodos adicionales específicos para Paciente si son necesarios
    Task<PacienteDTO> GetByPacienteIdAsync(string pacienteId);
}
