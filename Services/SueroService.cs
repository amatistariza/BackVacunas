using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models.Esquema;
using API.Persistence.Context;
using API.Persistence.Repositories;
using API.Services;

public class SueroService : BaseService<Suero>, ISueroService
{
    public SueroService(IRepository<Suero> repository) : base(repository)
    {
    }

    // Métodos específicos de sueros (si es necesario)
}