using API.Domain.DTOs;
using API.Domain.Models;

namespace API.Domain.IRepositories
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        // Métodos adicionales específicos para Paciente si son necesarios
        Task<PacienteDTO> GetByPacienteIdAsync(string pacienteId);
    }
}
