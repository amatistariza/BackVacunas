using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuidadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Parentesco = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndicativoTelefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuidadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diluyentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diluyentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jeringas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jeringas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Madres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndicativoTelefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    RegimenAfiliacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PertenenciaEtnica = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Desplazado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Madres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAtencion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EsquemaCompleto = table.Column<bool>(type: "bit", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    OrientacionSexual = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EdadGestacionalSemanas = table.Column<int>(type: "int", nullable: true),
                    PaisNacimiento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstatusMigratorio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LugarAtencionParto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegimenAfiliacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aseguradora = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PertenenciaEtnica = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Desplazado = table.Column<bool>(type: "bit", nullable: false),
                    Discapacitado = table.Column<bool>(type: "bit", nullable: false),
                    Fallecido = table.Column<bool>(type: "bit", nullable: false),
                    VictimaConflictoArmado = table.Column<bool>(type: "bit", nullable: false),
                    EstudiaActualmente = table.Column<bool>(type: "bit", nullable: false),
                    PaisResidencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartamentoResidencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MunicipioResidencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ComunaLocalidad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorizaLlamadasTelefonicas = table.Column<bool>(type: "bit", nullable: false),
                    AutorizaEnvioCorreo = table.Column<bool>(type: "bit", nullable: false),
                    MadreId = table.Column<int>(type: "int", nullable: true),
                    CuidadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "varchar(20)", nullable: false),
                    RolUser = table.Column<string>(type: "varchar(20)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacunas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Laboratorio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DosisDisponibles = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroDosis = table.Column<int>(type: "int", nullable: false),
                    IntervaloSemanas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacunas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Antecedentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ObservacionesEspeciales = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antecedentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Antecedentes_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntecedentesMedicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContraindicacionVacunacion = table.Column<bool>(type: "bit", nullable: false),
                    DetalleContraindicacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReaccionBiologicos = table.Column<bool>(type: "bit", nullable: false),
                    DetalleReaccion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedentesMedicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntecedentesMedicos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CondicionesUsuarias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condicion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gestante = table.Column<bool>(type: "bit", nullable: false),
                    FechaUltimaMenstruacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SemanasGestacion = table.Column<int>(type: "int", nullable: true),
                    FechaProbableParto = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CantidadEmbarazosPrevios = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicionesUsuarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CondicionesUsuarias_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EsquemasVacunacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoCarnet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Responsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegistradoPAI = table.Column<bool>(type: "bit", nullable: false),
                    MotivoIngreso = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    VacunaId = table.Column<int>(type: "int", nullable: false),
                    NumeroDeDosis = table.Column<int>(type: "int", nullable: false),
                    FechaDosisAplicada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaProximaDosis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ViaDeAdministracion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SitioDeAplicacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EsquemasVacunacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EsquemasVacunacion_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EsquemasVacunacion_Vacunas_VacunaId",
                        column: x => x.VacunaId,
                        principalTable: "Vacunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EsquemaVacunacionDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EsquemaVacunacionId = table.Column<int>(type: "int", nullable: false),
                    VacunaId = table.Column<int>(type: "int", nullable: true),
                    CantidadUtilizadaVacuna = table.Column<int>(type: "int", nullable: true),
                    DiluyenteId = table.Column<int>(type: "int", nullable: true),
                    CantidadUtilizadaDiluyente = table.Column<int>(type: "int", nullable: true),
                    JeringaId = table.Column<int>(type: "int", nullable: true),
                    CantidadUtilizadaJeringa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EsquemaVacunacionDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EsquemaVacunacionDetalles_Diluyentes_DiluyenteId",
                        column: x => x.DiluyenteId,
                        principalTable: "Diluyentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EsquemaVacunacionDetalles_EsquemasVacunacion_EsquemaVacunacionId",
                        column: x => x.EsquemaVacunacionId,
                        principalTable: "EsquemasVacunacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EsquemaVacunacionDetalles_Jeringas_JeringaId",
                        column: x => x.JeringaId,
                        principalTable: "Jeringas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EsquemaVacunacionDetalles_Vacunas_VacunaId",
                        column: x => x.VacunaId,
                        principalTable: "Vacunas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegistrosVacunacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    VacunaId = table.Column<int>(type: "int", nullable: false),
                    EsquemaVacunacionId = table.Column<int>(type: "int", nullable: false),
                    NumeroDosis = table.Column<int>(type: "int", nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaProximaDosis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoRegistro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UsuarioRegistro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosVacunacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosVacunacion_EsquemasVacunacion_EsquemaVacunacionId",
                        column: x => x.EsquemaVacunacionId,
                        principalTable: "EsquemasVacunacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrosVacunacion_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrosVacunacion_Vacunas_VacunaId",
                        column: x => x.VacunaId,
                        principalTable: "Vacunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlarmasVacunacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    VacunaId = table.Column<int>(type: "int", nullable: false),
                    DosisActual = table.Column<int>(type: "int", nullable: false),
                    FechaPrimeraAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaUltimaAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaProximaAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EsquemaCompletado = table.Column<bool>(type: "bit", nullable: false),
                    NotificacionEnviada = table.Column<bool>(type: "bit", nullable: false),
                    FechaNotificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EsquemaVacunacionDetalleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmasVacunacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmasVacunacion_EsquemaVacunacionDetalles_EsquemaVacunacionDetalleId",
                        column: x => x.EsquemaVacunacionDetalleId,
                        principalTable: "EsquemaVacunacionDetalles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlarmasVacunacion_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmasVacunacion_Vacunas_VacunaId",
                        column: x => x.VacunaId,
                        principalTable: "Vacunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmasVacunacion_EsquemaVacunacionDetalleId",
                table: "AlarmasVacunacion",
                column: "EsquemaVacunacionDetalleId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmasVacunacion_PacienteId",
                table: "AlarmasVacunacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmasVacunacion_VacunaId",
                table: "AlarmasVacunacion",
                column: "VacunaId");

            migrationBuilder.CreateIndex(
                name: "IX_Antecedentes_PacienteId",
                table: "Antecedentes",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedentesMedicos_PacienteId",
                table: "AntecedentesMedicos",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CondicionesUsuarias_PacienteId",
                table: "CondicionesUsuarias",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemasVacunacion_VacunaId",
                table: "EsquemasVacunacion",
                column: "VacunaId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaVacunacionDetalles_DiluyenteId",
                table: "EsquemaVacunacionDetalles",
                column: "DiluyenteId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaVacunacionDetalles_EsquemaVacunacionId",
                table: "EsquemaVacunacionDetalles",
                column: "EsquemaVacunacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaVacunacionDetalles_JeringaId",
                table: "EsquemaVacunacionDetalles",
                column: "JeringaId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaVacunacionDetalles_VacunaId",
                table: "EsquemaVacunacionDetalles",
                column: "VacunaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosVacunacion_EsquemaVacunacionId",
                table: "RegistrosVacunacion",
                column: "EsquemaVacunacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosVacunacion_PacienteId",
                table: "RegistrosVacunacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosVacunacion_VacunaId",
                table: "RegistrosVacunacion",
                column: "VacunaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmasVacunacion");

            migrationBuilder.DropTable(
                name: "Antecedentes");

            migrationBuilder.DropTable(
                name: "AntecedentesMedicos");

            migrationBuilder.DropTable(
                name: "CondicionesUsuarias");

            migrationBuilder.DropTable(
                name: "Cuidadores");

            migrationBuilder.DropTable(
                name: "Madres");

            migrationBuilder.DropTable(
                name: "RegistrosVacunacion");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "EsquemaVacunacionDetalles");

            migrationBuilder.DropTable(
                name: "Diluyentes");

            migrationBuilder.DropTable(
                name: "EsquemasVacunacion");

            migrationBuilder.DropTable(
                name: "Jeringas");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Vacunas");
        }
    }
}
