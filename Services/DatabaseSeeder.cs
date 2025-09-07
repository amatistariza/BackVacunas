using API.Domain.Models;
using API.Domain.Models.Esquema;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AplicationDbContext ctx)
    {
        // Asegura base creada
        await ctx.Database.MigrateAsync();

        if (!ctx.Usuarios.Any())
        {
            ctx.Usuarios.AddRange(
                new Usuario { NombreUsuario = "admin", RolUser = "ADMINISTRADOR", Password = "admin123" },
                new Usuario { NombreUsuario = "enfermero1", RolUser = "ENFERMERO", Password = "enf123" }
            );
            await ctx.SaveChangesAsync();
        }

        if (!ctx.Vacunas.Any())
        {
            ctx.Vacunas.AddRange(
                new Vacuna { Nombre = "BCG", Laboratorio = "LabBCG", Lote = "BCG-001", DosisDisponibles = 200, FechaRegistro = DateTime.UtcNow, NumeroDosis = 1, IntervaloSemanas = 0 },
                new Vacuna { Nombre = "Pentavalente", Laboratorio = "LabPenta", Lote = "PENTA-010", DosisDisponibles = 300, FechaRegistro = DateTime.UtcNow, NumeroDosis = 3, IntervaloSemanas = 8 },
                new Vacuna { Nombre = "Neumococo", Laboratorio = "LabNeo", Lote = "NEU-050", DosisDisponibles = 250, FechaRegistro = DateTime.UtcNow, NumeroDosis = 3, IntervaloSemanas = 8 },
                new Vacuna { Nombre = "Rotavirus", Laboratorio = "LabRota", Lote = "ROTA-021", DosisDisponibles = 180, FechaRegistro = DateTime.UtcNow, NumeroDosis = 2, IntervaloSemanas = 8 }
            );
            await ctx.SaveChangesAsync();
        }

        if (!ctx.Jeringas.Any())
        {
            ctx.Jeringas.AddRange(
                new Jeringa { Tipo = "23G1", Lote = "J-2301", CantidadDisponible = 500 },
                new Jeringa { Tipo = "22G1", Lote = "J-2201", CantidadDisponible = 400 }
            );
            await ctx.SaveChangesAsync();
        }

        if (!ctx.Diluyentes.Any())
        {
            ctx.Diluyentes.AddRange(
                new Diluyente { Nombre = "Cloruro Sodio 0.9%", Lote = "DIL-001", CantidadDisponible = 300 },
                new Diluyente { Nombre = "Agua Bacteriostatica", Lote = "DIL-002", CantidadDisponible = 200 }
            );
            await ctx.SaveChangesAsync();
        }

        if (!ctx.Sueros.Any())
        {
            ctx.Sueros.AddRange(
                new Suero { Nombre = "Antirrabico Humano", Lote = "SR-001", FrascosDisponibles = 100 },
                new Suero { Nombre = "Inmunoglobulina Tetánica", Lote = "SR-002", FrascosDisponibles = 60 }
            );
            await ctx.SaveChangesAsync();
        }

        if (!ctx.Madres.Any())
        {
            ctx.Madres.Add(new Madre
            {
                TipoIdentificacion = "CC",
                NumeroIdentificacion = "9001001",
                PrimerNombre = "Maria",
                PrimerApellido = "Lopez",
                CorreoElectronico = "maria@example.com",
                RegimenAfiliacion = "Contributivo",
                PertenenciaEtnica = "Mestizo",
                Desplazado = false
            });
            await ctx.SaveChangesAsync();
        }

        if (!ctx.Cuidadores.Any())
        {
            ctx.Cuidadores.Add(new Cuidador
            {
                TipoIdentificacion = "CC",
                NumeroIdentificacion = "8002001",
                PrimerNombre = "Carlos",
                PrimerApellido = "Gomez",
                Parentesco = "Padre",
                CorreoElectronico = "carlos@example.com"
            });
            await ctx.SaveChangesAsync();
        }

        // Asegurar al menos 2 pacientes (idempotente)
        if (!ctx.Pacientes.Any(p => p.NumeroIdentificacion == "1001"))
        {
            var madreId = ctx.Madres.OrderBy(m=>m.Id).First().Id;
            var cuidadorId = ctx.Cuidadores.OrderBy(c=>c.Id).First().Id;
            ctx.Pacientes.Add(new Paciente
            {
                FechaAtencion = DateTime.UtcNow,
                TipoIdentificacion = "RC",
                NumeroIdentificacion = "1001",
                PrimerNombre = "Ana",
                PrimerApellido = "Perez",
                FechaNacimiento = new DateTime(2024, 1, 15),
                EsquemaCompleto = false,
                Sexo = "F",
                PaisNacimiento = "Colombia",
                EstatusMigratorio = "Regular",
                RegimenAfiliacion = "Contributivo",
                Aseguradora = "Sura",
                PertenenciaEtnica = "Mestizo",
                Desplazado = false,
                Discapacitado = false,
                Fallecido = false,
                VictimaConflictoArmado = false,
                EstudiaActualmente = false,
                PaisResidencia = "Colombia",
                DepartamentoResidencia = "Antioquia",
                MunicipioResidencia = "Medellin",
                Area = "Urbana",
                AutorizaLlamadasTelefonicas = true,
                AutorizaEnvioCorreo = true,
                MadreId = madreId,
                CuidadorId = cuidadorId
            });
            await ctx.SaveChangesAsync();
        }
        if (!ctx.Pacientes.Any(p => p.NumeroIdentificacion == "1002"))
        {
            ctx.Pacientes.Add(new Paciente
            {
                FechaAtencion = DateTime.UtcNow,
                TipoIdentificacion = "RC",
                NumeroIdentificacion = "1002",
                PrimerNombre = "Luis",
                PrimerApellido = "Gomez",
                FechaNacimiento = new DateTime(2023, 11, 20),
                EsquemaCompleto = false,
                Sexo = "M",
                PaisNacimiento = "Colombia",
                EstatusMigratorio = "Regular",
                RegimenAfiliacion = "Subsidiado",
                Aseguradora = "NuevaEPS",
                PertenenciaEtnica = "Mestizo",
                Desplazado = false,
                Discapacitado = false,
                Fallecido = false,
                VictimaConflictoArmado = false,
                EstudiaActualmente = false,
                PaisResidencia = "Colombia",
                DepartamentoResidencia = "Antioquia",
                MunicipioResidencia = "Medellin",
                Area = "Urbana",
                AutorizaLlamadasTelefonicas = true,
                AutorizaEnvioCorreo = true
            });
            await ctx.SaveChangesAsync();
        }

        var paciente1 = ctx.Pacientes.OrderBy(p=>p.Id).First();
        var paciente2 = ctx.Pacientes.OrderBy(p=>p.Id).Skip(1).FirstOrDefault();

        if (!ctx.CondicionesUsuarias.Any(c=>c.PacienteId == paciente1.Id))
        {
            ctx.CondicionesUsuarias.Add(new CondicionUsuaria
            {
                Condicion = "Sin condicion",
                Gestante = false,
                CantidadEmbarazosPrevios = 0,
                PacienteId = paciente1.Id
            });
            await ctx.SaveChangesAsync();
        }

        if (!ctx.AntecedentesMedicos.Any(a=>a.PacienteId == paciente1.Id))
        {
            ctx.AntecedentesMedicos.Add(new AntecedentesMedicos
            {
                ContraindicacionVacunacion = false,
                ReaccionBiologicos = false,
                PacienteId = paciente1.Id
            });
            await ctx.SaveChangesAsync();
        }

        if (!ctx.Antecedentes.Any())
        {
            ctx.Antecedentes.Add(new Antecedente { FechaRegistro = DateTime.UtcNow, Tipo = "Medico", Descripcion = "Sin antecedentes relevantes", PacienteId = paciente1.Id });
            if (paciente2 != null)
            {
                ctx.Antecedentes.Add(new Antecedente { FechaRegistro = DateTime.UtcNow, Tipo = "Medico", Descripcion = "Sin antecedentes relevantes", PacienteId = paciente2.Id });
            }
            await ctx.SaveChangesAsync();
        }

        if (!ctx.EsquemasVacunacion.Any())
        {
            var vacunaPenta = ctx.Vacunas.First(v => v.Nombre == "Pentavalente");
            var esquema = new EsquemaVacunacion
            {
                TipoCarnet = "INFANTIL",
                Responsable = "Enfermera Juana",
                RegistradoPAI = true,
                PacienteId = paciente1.Id,
                VacunaId = vacunaPenta.Id,
                CantidadTotalDosis = 3,
                FrecuenciaAplicacion = "semanal",
                FechaPrimeraDosis = DateTime.UtcNow
            };
            ctx.EsquemasVacunacion.Add(esquema);
            await ctx.SaveChangesAsync();

            var detalle = new EsquemaVacunacionDetalle
            {
                EsquemaVacunacionId = esquema.Id,
                VacunaId = vacunaPenta.Id,
                CantidadUtilizadaVacuna = 1,
                FechaAplicacion = DateTime.UtcNow,
                NumeroDosis = 1,
                JeringaId = ctx.Jeringas.OrderBy(j=>j.Id).First().Id
            };
            ctx.EsquemaVacunacionDetalles.Add(detalle);
            await ctx.SaveChangesAsync();

            ctx.AlarmasVacunacion.Add(new AlarmaVacunacion
            {
                PacienteId = paciente1.Id,
                VacunaId = vacunaPenta.Id,
                DosisActual = 1,
                FechaPrimeraAplicacion = detalle.FechaAplicacion,
                FechaUltimaAplicacion = detalle.FechaAplicacion,
                FechaProximaAplicacion = detalle.FechaAplicacion.AddDays(vacunaPenta.IntervaloSemanas * 7),
                Observaciones = "Proxima dosis Pentavalente",
                EsquemaVacunacionDetalleId = detalle.Id
            });
            await ctx.SaveChangesAsync();

            ctx.RegistrosVacunacion.Add(new RegistroVacunacion
            {
                PacienteId = paciente1.Id,
                VacunaId = vacunaPenta.Id,
                EsquemaVacunacionId = esquema.Id,
                NumeroDosis = 1,
                FechaAplicacion = detalle.FechaAplicacion,
                FechaProximaDosis = detalle.FechaAplicacion.AddDays(vacunaPenta.IntervaloSemanas * 7),
                FechaRegistro = DateTime.UtcNow,
                EstadoRegistro = "Aplicada",
                Observaciones = "Primera dosis aplicada",
                UsuarioRegistro = "admin"
            });
            await ctx.SaveChangesAsync();

            if (paciente2 != null)
            {
                var vacunaNeumo = ctx.Vacunas.First(v => v.Nombre == "Neumococo");
                ctx.EsquemasVacunacion.Add(new EsquemaVacunacion
                {
                    TipoCarnet = "INFANTIL",
                    Responsable = "Enfermero Carlos",
                    RegistradoPAI = true,
                    PacienteId = paciente2.Id,
                    VacunaId = vacunaNeumo.Id,
                    CantidadTotalDosis = 3,
                    FrecuenciaAplicacion = "semanal",
                    FechaPrimeraDosis = DateTime.UtcNow
                });
                await ctx.SaveChangesAsync();
            }
        }
    }
}
