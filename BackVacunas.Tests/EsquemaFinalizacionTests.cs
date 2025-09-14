using System;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Domain.Models.Esquema;
using API.Persistence.Context;
using API.Persistence.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BackVacunas.Tests;

public class EsquemaFinalizacionTests
{
    private AplicationDbContext BuildContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AplicationDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        return new AplicationDbContext(options);
    }

    private EsquemaVacunacionService BuildService(AplicationDbContext ctx)
    {
        var esquemaRepo = new EsquemaVacunacionRepository(ctx);
        var vacunaRepo = new VacunaRepository(ctx);
        var diluyenteRepo = new DiluyenteRepository(ctx);
        var jeringaRepo = new JeringaRepository(ctx);
        var pacienteRepo = new PacienteRepository(ctx);
        var alarmaRepo = new AlarmaVacunacionRepository(ctx);
        var alarmaService = new AlarmaVacunacionService(alarmaRepo, vacunaRepo, esquemaRepo);
        return new EsquemaVacunacionService(esquemaRepo, vacunaRepo, diluyenteRepo, jeringaRepo, pacienteRepo, alarmaService);
    }

    private async Task SeedPacienteYVacunaAsync(AplicationDbContext ctx, int numeroDosis)
    {
        ctx.Pacientes.Add(new Paciente
        {
            FechaAtencion = DateTime.Today,
            TipoIdentificacion = "RC",
            NumeroIdentificacion = "TEST",
            PrimerNombre = "Ana",
            PrimerApellido = "Prueba",
            FechaNacimiento = new DateTime(2024, 1, 1),
            EsquemaCompleto = false,
            Sexo = "F",
            PaisNacimiento = "CO",
            EstatusMigratorio = "Reg",
            RegimenAfiliacion = "Contrib",
            Aseguradora = "EPS",
            PertenenciaEtnica = "Mestizo",
            Desplazado = false,
            Discapacitado = false,
            Fallecido = false,
            VictimaConflictoArmado = false,
            EstudiaActualmente = false,
            PaisResidencia = "CO",
            DepartamentoResidencia = "Antioquia",
            MunicipioResidencia = "Medellin",
            Area = "Urbana",
            AutorizaLlamadasTelefonicas = true,
            AutorizaEnvioCorreo = true
        });
        await ctx.SaveChangesAsync();

        ctx.Vacunas.Add(new Vacuna
        {
            Nombre = "VacunaX",
            Laboratorio = "Lab",
            Lote = "L-001",
            DosisDisponibles = 100,
            FechaRegistro = DateTime.Today,
            NumeroDosis = numeroDosis,
            IntervaloSemanas = 4
        });
        await ctx.SaveChangesAsync();
    }

    [Fact]
    public async Task ConTresDosis_FinalizaEnTercera_NoCreaProximaNiAlarma()
    {
        var dbName = Guid.NewGuid().ToString();
        using var ctx = BuildContext(dbName);
        await SeedPacienteYVacunaAsync(ctx, 3);
        var service = BuildService(ctx);

        // Primera dosis
        await service.RegistrarEsquemaAsync(new EsquemaVacunacion
        {
            TipoCarnet = "INFANTIL",
            Responsable = "Enf",
            RegistradoPAI = true,
            PacienteId = 1,
            VacunaId = 1,
            Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
        });
        // Segunda dosis
        await service.RegistrarEsquemaAsync(new EsquemaVacunacion
        {
            TipoCarnet = "INFANTIL",
            Responsable = "Enf",
            RegistradoPAI = true,
            PacienteId = 1,
            VacunaId = 1,
            Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
        });
        // Tercera dosis (última)
        await service.RegistrarEsquemaAsync(new EsquemaVacunacion
        {
            TipoCarnet = "INFANTIL",
            Responsable = "Enf",
            RegistradoPAI = true,
            PacienteId = 1,
            VacunaId = 1,
            Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
        });

        var esquemas = ctx.EsquemasVacunacion.OrderBy(e => e.Id).ToList();
        Assert.Equal(3, esquemas.Count);
        Assert.Equal(1, esquemas[0].NumeroDeDosis);
        Assert.Equal(2, esquemas[1].NumeroDeDosis);
        Assert.Equal(3, esquemas[2].NumeroDeDosis);
        Assert.Null(esquemas[2].FechaProximaDosis);

        // No debe existir alarma pendiente para esa vacuna/paciente
        var alarmaPendiente = await ctx.AlarmasVacunacion
            .FirstOrDefaultAsync(a => a.PacienteId == 1 && a.VacunaId == 1 && !a.EsquemaCompletado);
        Assert.Null(alarmaPendiente);
    }

    [Fact]
    public async Task ConCuatroDosis_FinalizaEnCuarta_NoCreaProximaNiAlarma()
    {
        var dbName = Guid.NewGuid().ToString();
        using var ctx = BuildContext(dbName);
        await SeedPacienteYVacunaAsync(ctx, 4);
        var service = BuildService(ctx);

        // Aplicar 4 dosis
        for (int i = 0; i < 4; i++)
        {
            await service.RegistrarEsquemaAsync(new EsquemaVacunacion
            {
                TipoCarnet = "INFANTIL",
                Responsable = "Enf",
                RegistradoPAI = true,
                PacienteId = 1,
                VacunaId = 1,
                Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
            });
        }

        var ultimo = ctx.EsquemasVacunacion.OrderByDescending(e => e.Id).First();
        Assert.Equal(4, ultimo.NumeroDeDosis);
        Assert.Null(ultimo.FechaProximaDosis);

        // No debe existir alarma pendiente para esa vacuna/paciente
        var alarmaPendiente = await ctx.AlarmasVacunacion
            .FirstOrDefaultAsync(a => a.PacienteId == 1 && a.VacunaId == 1 && !a.EsquemaCompletado);
        Assert.Null(alarmaPendiente);
    }

    // Con esquema de 3 dosis, intentar una 4ta debe fallar
    [Fact]
    public async Task ConTresDosis_IntentarCuartaDebeFallar()
    {
        var dbName = Guid.NewGuid().ToString();
        using var ctx = BuildContext(dbName);
        await SeedPacienteYVacunaAsync(ctx, 3);
        var service = BuildService(ctx);

        // Aplicar 3 dosis válidas
        for (int i = 0; i < 3; i++)
        {
            await service.RegistrarEsquemaAsync(new EsquemaVacunacion
            {
                TipoCarnet = "INFANTIL",
                Responsable = "Enf",
                RegistradoPAI = true,
                PacienteId = 1,
                VacunaId = 1,
                Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
            });
        }

        // Intentar cuarta dosis debe lanzar excepción
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await service.RegistrarEsquemaAsync(new EsquemaVacunacion
            {
                TipoCarnet = "INFANTIL",
                Responsable = "Enf",
                RegistradoPAI = true,
                PacienteId = 1,
                VacunaId = 1,
                Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
            });
        });
        Assert.Equal("El esquema de vacunación ya está completo para esta vacuna.", ex.Message);

        // Verificar que no se creó un 4to registro
        Assert.Equal(3, ctx.EsquemasVacunacion.Count());
        var ultimo = ctx.EsquemasVacunacion.OrderByDescending(e => e.Id).First();
        Assert.Equal(3, ultimo.NumeroDeDosis);
        Assert.Null(ultimo.FechaProximaDosis);

        // No debe existir alarma pendiente para esa vacuna/paciente
        var alarmaPendiente = await ctx.AlarmasVacunacion
            .FirstOrDefaultAsync(a => a.PacienteId == 1 && a.VacunaId == 1 && !a.EsquemaCompletado);
        Assert.Null(alarmaPendiente);
    }

    // test para probar que no deje aplicar dosis si ya se terminó el esquema
    [Fact]
    public async Task ConTresDosis_NoPermiteRegistrarCuartaDosis()
    {
        var dbName = Guid.NewGuid().ToString();
        using var ctx = BuildContext(dbName);
        await SeedPacienteYVacunaAsync(ctx, 3);
        var service = BuildService(ctx);

        // Aplicar 3 dosis
        for (int i = 0; i < 3; i++)
        {
            await service.RegistrarEsquemaAsync(new EsquemaVacunacion
            {
                TipoCarnet = "INFANTIL",
                Responsable = "Enf",
                RegistradoPAI = true,
                PacienteId = 1,
                VacunaId = 1,
                Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
            });
        }

        // Intentar aplicar una cuarta dosis y esperar excepción
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await service.RegistrarEsquemaAsync(new EsquemaVacunacion
            {
                TipoCarnet = "INFANTIL",
                Responsable = "Enf",
                RegistradoPAI = true,
                PacienteId = 1,
                VacunaId = 1,
                Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
            });
        });

        Assert.Equal("El esquema de vacunación ya está completo para esta vacuna.", ex.Message);

        var totalDosis = ctx.EsquemasVacunacion.Count();
        Assert.Equal(3, totalDosis); // Solo deben existir 3 dosis registradas
    }
   
}
