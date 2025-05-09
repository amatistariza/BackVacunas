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
    [Migration("20241128182333_EsquemaVacunacion")]
    partial class EsquemaVacunacion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("API.Domain.Models.Esquema.MovimientoInventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaMovimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecursoId")
                        .HasColumnType("int");

                    b.Property<string>("TipoMovimiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoRecurso")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovimientosInventario");
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

            modelBuilder.Entity("API.Domain.Models.Esquema.TipoDosis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TiposDosis");
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

            modelBuilder.Entity("API.Domain.Models.Esquema.VacunaAdministrada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DiluyenteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaAdministracion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FrascosUtilizados")
                        .HasColumnType("int");

                    b.Property<int>("JeringaId")
                        .HasColumnType("int");

                    b.Property<string>("LoteDiluyente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoteJeringa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoteVacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<int?>("SueroId")
                        .HasColumnType("int");

                    b.Property<int>("TipoDosisId")
                        .HasColumnType("int");

                    b.Property<int>("VacunaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiluyenteId");

                    b.HasIndex("JeringaId");

                    b.HasIndex("SueroId");

                    b.HasIndex("TipoDosisId");

                    b.HasIndex("VacunaId");

                    b.ToTable("VacunasAdministradas");
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

            modelBuilder.Entity("API.Domain.Models.Esquema.VacunaAdministrada", b =>
                {
                    b.HasOne("API.Domain.Models.Esquema.Diluyente", "Diluyente")
                        .WithMany()
                        .HasForeignKey("DiluyenteId");

                    b.HasOne("API.Domain.Models.Esquema.Jeringa", "Jeringa")
                        .WithMany()
                        .HasForeignKey("JeringaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Domain.Models.Esquema.Suero", "Suero")
                        .WithMany()
                        .HasForeignKey("SueroId");

                    b.HasOne("API.Domain.Models.Esquema.TipoDosis", "TipoDosis")
                        .WithMany()
                        .HasForeignKey("TipoDosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Domain.Models.Esquema.Vacuna", "Vacuna")
                        .WithMany()
                        .HasForeignKey("VacunaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diluyente");

                    b.Navigation("Jeringa");

                    b.Navigation("Suero");

                    b.Navigation("TipoDosis");

                    b.Navigation("Vacuna");
                });
#pragma warning restore 612, 618
        }
    }
}
