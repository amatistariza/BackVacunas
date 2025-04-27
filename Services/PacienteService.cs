using API.Domain.DTOs;
using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;

namespace API.Services;

public class PacienteService : BaseService<Paciente>, IPacienteService
{
    private readonly IPacienteRepository pacienteRepository;

    public PacienteService(IPacienteRepository pacienteRepository) : base(pacienteRepository)
    {
        this.pacienteRepository = pacienteRepository;
    }

    public async Task<PacienteDTO> GetByPacienteIdAsync(string pacienteId)
    {
        return await pacienteRepository.GetByPacienteIdAsync(pacienteId);
    }

    // Implementar métodos adicionales específicos para Paciente si son necesarios
}