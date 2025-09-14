using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using API.Persistence.Context;
using API.Domain.Models; // Paciente, EsquemaVacunacion, EsquemaVacunacionDetalle
using API.Domain.Models.Esquema; // Vacuna y otros insumos en subcarpeta Esquema
using API.Domain.IRepositories;
using API.Persistence.Repositories;
using API.Services;

namespace BackVacunas.Tests;

public class EsquemaVacunacionValidacionTests
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

    private async Task SeedBaseAsync(AplicationDbContext ctx)
    {
        ctx.Pacientes.Add(new Paciente
        {
            FechaAtencion = DateTime.UtcNow,
            TipoIdentificacion = "RC",
            NumeroIdentificacion = "P1",
            PrimerNombre = "Test",
            PrimerApellido = "Paciente",
            FechaNacimiento = new DateTime(2024,1,1),
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
            Nombre = "Pentavalente",
            Laboratorio = "Lab",
            Lote = "L-001",
            DosisDisponibles = 100,
            FechaRegistro = DateTime.UtcNow,
            NumeroDosis = 3,
            IntervaloSemanas = 8
        });
        await ctx.SaveChangesAsync();
    }

    [Fact]
    public async Task PrimeraDosis_Aplica()
    {
        var dbName = Guid.NewGuid().ToString();
        using var ctx = BuildContext(dbName);
        await SeedBaseAsync(ctx);
        var service = BuildService(ctx);

        var result = await service.ValidarAplicacionDosisAsync(1, 1);

        Assert.True(result.aplica);
        Assert.Equal(1, result.numeroDosis);
        Assert.Contains("primera", result.mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task SegundaDosis_TodaviaNoCorresponde()
    {
        var dbName = Guid.NewGuid().ToString();
        using var ctx = BuildContext(dbName);
        await SeedBaseAsync(ctx);
        var service = BuildService(ctx);

        // Registrar primera dosis con fecha reciente (hoy)
        ctx.EsquemasVacunacion.Add(new EsquemaVacunacion
        {
            TipoCarnet = "INFANTIL",
            Responsable = "Enf",
            RegistradoPAI = true,
            PacienteId = 1,
            VacunaId = 1,
            NumeroDeDosis = 1,
            FechaDosisAplicada = DateTime.UtcNow,
            FechaProximaDosis = DateTime.UtcNow.AddDays(56),
            ViaDeAdministracion = "IM",
            SitioDeAplicacion = "Muslo",
            Lote = "L-001",
            Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
        });
        await ctx.SaveChangesAsync();

        var result = await service.ValidarAplicacionDosisAsync(1, 1);

        Assert.False(result.aplica);
        Assert.Equal(2, result.numeroDosis);
        Assert.Contains("Todavía no corresponde", result.mensaje, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task EsquemaFinalizado()
    {
        var dbName = Guid.NewGuid().ToString();
        using var ctx = BuildContext(dbName);
        await SeedBaseAsync(ctx);
        var service = BuildService(ctx);

        // Registrar tercera dosis (última)
        ctx.EsquemasVacunacion.Add(new EsquemaVacunacion
        {
            TipoCarnet = "INFANTIL",
            Responsable = "Enf",
            RegistradoPAI = true,
            PacienteId = 1,
            VacunaId = 1,
            NumeroDeDosis = 3,
            FechaDosisAplicada = DateTime.UtcNow.AddDays(-120),
            FechaProximaDosis = null,
            ViaDeAdministracion = "IM",
            SitioDeAplicacion = "Muslo",
            Lote = "L-001",
            Detalles = new System.Collections.Generic.List<EsquemaVacunacionDetalle>()
        });
        await ctx.SaveChangesAsync();

        var result = await service.ValidarAplicacionDosisAsync(1, 1);

        Assert.False(result.aplica);
        Assert.Equal(3, result.numeroDosis);
        Assert.Contains("finalizado", result.mensaje, StringComparison.OrdinalIgnoreCase);
    }
}
