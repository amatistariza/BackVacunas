using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;

namespace API.Services;

public class MadreService : BaseService<Madre>, IMadreService
{
    public MadreService(IMadreRepository madreRepository) : base(madreRepository)
    {
    }

    // Métodos específicos para Madre (si son necesarios en el futuro)
}