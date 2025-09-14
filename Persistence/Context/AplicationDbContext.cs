using API.Domain.Models;
using API.Domain.Models.Esquema;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Context;

public class AplicationDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Vacuna> Vacunas { get; set; }
    public DbSet<Jeringa> Jeringas { get; set; }
    public DbSet<Diluyente> Diluyentes { get; set; }
    public DbSet<Madre> Madres { get; set; }
    public DbSet<Cuidador> Cuidadores { get; set; }
    public DbSet<Antecedente> Antecedentes { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<CondicionUsuaria> CondicionesUsuarias { get; set; }
    public DbSet<AntecedentesMedicos> AntecedentesMedicos { get; set; }
    public DbSet<EsquemaVacunacion> EsquemasVacunacion { get; set; }
    public DbSet<EsquemaVacunacionDetalle> EsquemaVacunacionDetalles { get; set; }
    public DbSet<AlarmaVacunacion> AlarmasVacunacion { get; set; }
    public DbSet<RegistroVacunacion> RegistrosVacunacion { get; set; }

    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Antecedente>().ToTable("Antecedentes");
        modelBuilder.Entity<Paciente>().ToTable("Pacientes");
        // Índice único por tipo+numero de identificación para evitar duplicados
        modelBuilder.Entity<Paciente>()
            .HasIndex(p => new { p.TipoIdentificacion, p.NumeroIdentificacion })
            .IsUnique();

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.Antecedentes)
            .WithOne(a => a.Paciente)
            .HasForeignKey(a => a.PacienteId);

        //modelBuilder.Entity<Madre>()
        //    .HasMany(m => m.Pacientes)
        //    .WithOne(p => p.Madre)
        //    .HasForeignKey(p => p.MadreId);

        //modelBuilder.Entity<Cuidador>()
        //    .HasMany(c => c.Pacientes)
        //    .WithOne(p => p.Cuidador)
        //    .HasForeignKey(p => p.CuidadorId);

        modelBuilder.Entity<Paciente>()
             .HasOne(p => p.CondicionUsuaria)
             .WithOne(c => c.Paciente)
             .HasForeignKey<CondicionUsuaria>(c => c.PacienteId); // Configura la clave externa

        modelBuilder.Entity<Paciente>()
            .HasOne(p => p.AntecedentesMedicos)
            .WithOne(a => a.Paciente)
            .HasForeignKey<AntecedentesMedicos>(a => a.PacienteId); // Configura la clave externa

        // Configuraciones para evitar cascadas circulares
        modelBuilder.Entity<EsquemaVacunacion>()
            .HasOne(e => e.Paciente)
            .WithMany()
            .HasForeignKey(e => e.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EsquemaVacunacion>()
            .HasOne(e => e.Vacuna)
            .WithMany()
            .HasForeignKey(e => e.VacunaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RegistroVacunacion>()
            .HasOne(r => r.Paciente)
            .WithMany()
            .HasForeignKey(r => r.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RegistroVacunacion>()
            .HasOne(r => r.Vacuna)
            .WithMany()
            .HasForeignKey(r => r.VacunaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RegistroVacunacion>()
            .HasOne(r => r.EsquemaVacunacion)
            .WithMany()
            .HasForeignKey(r => r.EsquemaVacunacionId)
            .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Paciente>()
        //  .HasOne(p => p.EsquemaVacunacion)
        //  .WithOne(e => e.Paciente)
        //  .HasForeignKey<EsquemaVacunacion>(e => e.PacienteId); // Clave externa de EsquemaVacunacion

        //modelBuilder.Entity<EsquemaVacunacion>()
        //    .HasMany(e => e.Detalles)
        //    .WithOne(d => d.EsquemaVacunacion)
        //    .HasForeignKey(d => d.EsquemaVacunacionId); // Clave externa de EsquemaVacunacionDetalle


    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    // Configuraciones adicionales
    //    modelBuilder.Entity<VacunaAdministrada>()
    //        .HasOne(va => va.Vacuna)
    //        .WithMany()
    //        .HasForeignKey(va => va.VacunaId);

    //    modelBuilder.Entity<VacunaAdministrada>()
    //        .HasOne(va => va.Jeringa)
    //        .WithMany()
    //        .HasForeignKey(va => va.JeringaId);

    //    modelBuilder.Entity<VacunaAdministrada>()
    //        .HasOne(va => va.Diluyente)
    //        .WithMany()
    //        .HasForeignKey(va => va.DiluyenteId);


    //    modelBuilder.Entity<VacunaAdministrada>()
    //        .HasOne(va => va.TipoDosis)
    //        .WithMany()
    //        .HasForeignKey(va => va.TipoDosisId);
    //}
}