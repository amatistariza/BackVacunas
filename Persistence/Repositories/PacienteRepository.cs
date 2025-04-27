using API.Domain.DTOs;
using API.Domain.IRepositories;
using API.Domain.Models;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repositories
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        private readonly AplicationDbContext _context;


        public PacienteRepository(AplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PacienteDTO> GetByPacienteIdAsync(string pacienteId)
        {
            var result = await _context.Pacientes
                .Include(e => e.Antecedentes)
                .Include(e => e.CondicionUsuaria)
                .Include(e => e.AntecedentesMedicos)
                .Where(e => e.NumeroIdentificacion == pacienteId)
                .Select(e => new PacienteDTO
                {
                    Id = e.Id,
                    FechaAtencion = e.FechaAtencion,
                    TipoIdentificacion = e.TipoIdentificacion,
                    NumeroIdentificacion = e.NumeroIdentificacion,
                    PrimerNombre = e.PrimerNombre,
                    SegundoNombre = e.SegundoNombre,
                    PrimerApellido = e.PrimerApellido,
                    SegundoApellido = e.SegundoApellido,
                    FechaNacimiento = e.FechaNacimiento,
                    EsquemaCompleto = e.EsquemaCompleto,
                    Sexo = e.Sexo,
                    Genero = e.Genero,
                    OrientacionSexual = e.OrientacionSexual,
                    EdadGestacionalSemanas = e.EdadGestacionalSemanas,
                    PaisNacimiento = e.PaisNacimiento,
                    EstatusMigratorio = e.EstatusMigratorio,
                    LugarAtencionParto = e.LugarAtencionParto,
                    RegimenAfiliacion = e.RegimenAfiliacion,
                    Aseguradora = e.Aseguradora,
                    PertenenciaEtnica = e.PertenenciaEtnica,
                    Desplazado = e.Desplazado,
                    Discapacitado = e.Discapacitado,
                    Fallecido = e.Fallecido,
                    VictimaConflictoArmado = e.VictimaConflictoArmado,
                    EstudiaActualmente = e.EstudiaActualmente,
                    PaisResidencia = e.PaisResidencia,
                    DepartamentoResidencia = e.DepartamentoResidencia,
                    MunicipioResidencia = e.MunicipioResidencia,
                    ComunaLocalidad = e.ComunaLocalidad,
                    Area = e.Area,
                    Direccion = e.Direccion,
                    IndicativoTelefono = e.IndicativoTelefono,
                    TelefonoFijo = e.TelefonoFijo,
                    Celular = e.Celular,
                    Email = e.Email,
                    AutorizaLlamadasTelefonicas = e.AutorizaLlamadasTelefonicas,
                    AutorizaEnvioCorreo = e.AutorizaEnvioCorreo,
                    Antecedentes = e.Antecedentes.Select(a => new AntecedenteDTO
                    {
                        Id = a.Id,
                        Tipo = a.Tipo,
                        Descripcion = a.Descripcion,
                        FechaRegistro = a.FechaRegistro
                    }).ToList(),
                    MadreId = e.MadreId,
                    CuidadorId = e.CuidadorId,
                    CondicionUsuaria = new CondicionUsuariaDTO()
                    {
                        Id = e.CondicionUsuaria.Id,
                        Condicion = e.CondicionUsuaria.Condicion,
                        Gestante = e.CondicionUsuaria.Gestante,
                        FechaUltimaMenstruacion = e.CondicionUsuaria.FechaUltimaMenstruacion,
                        SemanasGestacion = e.CondicionUsuaria.SemanasGestacion,
                        FechaProbableParto = e.CondicionUsuaria.FechaProbableParto,
                        CantidadEmbarazosPrevios = e.CondicionUsuaria.CantidadEmbarazosPrevios
                    },
                    AntecedentesMedicos = new AntecedentesMedicosDTO()
                    {
                        Id = e.AntecedentesMedicos.Id,
                        ContraindicacionVacunacion = e.AntecedentesMedicos.ContraindicacionVacunacion,
                        DetalleContraindicacion = e.AntecedentesMedicos.DetalleContraindicacion,
                        ReaccionBiologicos = e.AntecedentesMedicos.ReaccionBiologicos,
                        DetalleReaccion = e.AntecedentesMedicos.DetalleReaccion
                    }
                })
                .FirstOrDefaultAsync();

            if (result != null)
            {
                var madre = await _context.Madres
                    .Where(m => m.Id == result.MadreId)
                    .Select(m => new MadreDTO
                    {
                        Id = m.Id,
                        TipoIdentificacion = m.TipoIdentificacion,
                        NumeroIdentificacion = m.NumeroIdentificacion,
                        PrimerNombre = m.PrimerNombre,
                        SegundoNombre = m.SegundoNombre,
                        PrimerApellido = m.PrimerApellido,
                        SegundoApellido = m.SegundoApellido,
                        CorreoElectronico = m.CorreoElectronico,
                        IndicativoTelefono = m.IndicativoTelefono,
                        TelefonoFijo = m.TelefonoFijo,
                        Celular = m.Celular,
                        RegimenAfiliacion = m.RegimenAfiliacion,
                        PertenenciaEtnica = m.PertenenciaEtnica,
                        Desplazado = m.Desplazado
                    })
                    .FirstOrDefaultAsync();

                var cuidador = await _context.Cuidadores
                    .Where(c => c.Id == result.CuidadorId)
                    .Select(c => new CuidadorDTO
                    {
                        Id = c.Id,
                        TipoIdentificacion = c.TipoIdentificacion,
                        NumeroIdentificacion = c.NumeroIdentificacion,
                        PrimerNombre = c.PrimerNombre,
                        SegundoNombre = c.SegundoNombre,
                        PrimerApellido = c.PrimerApellido,
                        SegundoApellido = c.SegundoApellido,
                        Parentesco = c.Parentesco,
                        CorreoElectronico = c.CorreoElectronico,
                        IndicativoTelefono = c.IndicativoTelefono,
                        TelefonoFijo = c.TelefonoFijo,
                        Celular = c.Celular
                    })
                    .FirstOrDefaultAsync();

                //var esquema = await _context.EsquemasVacunacion
                //    .Where(x => x.PacienteId == result.Id)
                //    .Select(x => new EsquemaVacunaDTO
                //    {
                //        Id = x.Id,
                //        TipoCarnet = x.TipoCarnet,
                //        Responsable = x.Responsable,
                //        RegistradoPAI = x.RegistradoPAI,
                //        MotivoNoIngreso = x.MotivoNoIngreso,
                //        Observaciones = x.Observaciones,
                //        Detalles = x.Detalles
                //    })
                //    .FirstOrDefaultAsync();

                result.Madre = madre;
                result.Cuidador = cuidador;
               // result.EsquemaVacuna = esquema;
            }

            return result;
        }
    }
}
