﻿// <auto-generated />
using System;
using API.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20241202175208_ConfiguracionPaciente")]
    partial class ConfiguracionPaciente
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Domain.Models.Antecedente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("ObservacionesEspeciales")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.ToTable("Antecedentes", (string)null);
                });

            modelBuilder.Entity("API.Domain.Models.Cuidador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Celular")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IndicativoTelefono")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("NumeroIdentificacion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Parentesco")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrimerNombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SegundoApellido")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SegundoNombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TelefonoFijo")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("TipoIdentificacion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Cuidadores");
                });

            modelBuilder.Entity("API.Domain.Models.Esquema.Diluyente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadDisponible")
                        .HasColumnType("int");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Diluyentes");
                });

            modelBuilder.Entity("API.Domain.Models.Esquema.Jeringa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadDisponible")
                        .HasColumnType("int");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Jeringas");
                });

            modelBuilder.Entity("API.Domain.Models.Esquema.Suero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FrascosDisponibles")
                        .HasColumnType("int");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sueros");
                });

            modelBuilder.Entity("API.Domain.Models.Esquema.Vacuna", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DosisDisponibles")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Laboratorio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vacunas");
                });

            modelBuilder.Entity("API.Domain.Models.Madre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Celular")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Desplazado")
                        .HasColumnType("bit");

                    b.Property<string>("IndicativoTelefono")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("NumeroIdentificacion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PertenenciaEtnica")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrimerNombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RegimenAfiliacion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SegundoApellido")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SegundoNombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TelefonoFijo")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("TipoIdentificacion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Madres");
                });

            modelBuilder.Entity("API.Domain.Models.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Aseguradora")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("AutorizaEnvioCorreo")
                        .HasColumnType("bit");

                    b.Property<bool>("AutorizaLlamadasTelefonicas")
                        .HasColumnType("bit");

                    b.Property<string>("Celular")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("ComunaLocalidad")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CuidadorId")
                        .HasColumnType("int");

                    b.Property<string>("DepartamentoResidencia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Desplazado")
                        .HasColumnType("bit");

                    b.Property<string>("Direccion")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Discapacitado")
                        .HasColumnType("bit");

                    b.Property<int?>("EdadGestacionalSemanas")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsquemaCompleto")
                        .HasColumnType("bit");

                    b.Property<string>("EstatusMigratorio")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EstudiaActualmente")
                        .HasColumnType("bit");

                    b.Property<bool>("Fallecido")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaAtencion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("IndicativoTelefono")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LugarAtencionParto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("MadreId")
                        .HasColumnType("int");

                    b.Property<string>("MunicipioResidencia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NumeroIdentificacion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("OrientacionSexual")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaisNacimiento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaisResidencia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PertenenciaEtnica")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrimerNombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RegimenAfiliacion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SegundoApellido")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SegundoNombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TelefonoFijo")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("TipoIdentificacion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("VictimaConflictoArmado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CuidadorId");

                    b.HasIndex("MadreId");

                    b.ToTable("Pacientes", (string)null);
                });

            modelBuilder.Entity("API.Domain.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("RolUser")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("API.Domain.Models.Antecedente", b =>
                {
                    b.HasOne("API.Domain.Models.Paciente", "Paciente")
                        .WithMany("Antecedentes")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("API.Domain.Models.Paciente", b =>
                {
                    b.HasOne("API.Domain.Models.Cuidador", "Cuidador")
                        .WithMany("Pacientes")
                        .HasForeignKey("CuidadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Domain.Models.Madre", "Madre")
                        .WithMany("Pacientes")
                        .HasForeignKey("MadreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuidador");

                    b.Navigation("Madre");
                });

            modelBuilder.Entity("API.Domain.Models.Cuidador", b =>
                {
                    b.Navigation("Pacientes");
                });

            modelBuilder.Entity("API.Domain.Models.Madre", b =>
                {
                    b.Navigation("Pacientes");
                });

            modelBuilder.Entity("API.Domain.Models.Paciente", b =>
                {
                    b.Navigation("Antecedentes");
                });
#pragma warning restore 612, 618
        }
    }
}
