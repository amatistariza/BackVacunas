using API.Domain.Models;

namespace API.Domain.IServices
{
    public interface IMadreService : IBaseService<Madre>
    {
        Task<Madre> GetByIdentificacionAsync(string tipoIdentificacion, string numeroIdentificacion);
    }
}
