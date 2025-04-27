using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models.Esquema;
using API.Persistence.Context;
using API.Persistence.Repositories;
using API.Services;

public class JeringaService : BaseService<Jeringa>, IJeringaService
{
    public JeringaService(IRepository<Jeringa> repository) : base(repository)
    {
    }

    // Métodos específicos de jeringas (si es necesario)
}