using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;

namespace API.Services;

public class MadreService : BaseService<Madre>, IMadreService
{
    private readonly IMadreRepository _madreRepository;

    public MadreService(IMadreRepository madreRepository) : base(madreRepository)
    {
        _madreRepository = madreRepository;
    }

    // Métodos específicos para Madre (si son necesarios en el futuro)
    public Task<Madre> GetByIdentificacionAsync(string tipoIdentificacion, string numeroIdentificacion)
        => _madreRepository.GetByIdentificacionAsync(tipoIdentificacion, numeroIdentificacion);
}