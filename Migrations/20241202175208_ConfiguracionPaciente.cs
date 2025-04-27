using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracionPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Genero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OrientacionSexual = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EdadGestacionalSemanas = table.Column<int>(type: "int", nullable: true),
                    PaisNacimiento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstatusMigratorio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LugarAtencionParto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    IndicativoTelefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorizaLlamadasTelefonicas = table.Column<bool>(type: "bit", nullable: false),
                    AutorizaEnvioCorreo = table.Column<bool>(type: "bit", nullable: false),
                    MadreId = table.Column<int>(type: "int", nullable: false),
                    CuidadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Cuidadores_CuidadorId",
                        column: x => x.CuidadorId,
                        principalTable: "Cuidadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pacientes_Madres_MadreId",
                        column: x => x.MadreId,
                        principalTable: "Madres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_Antecedentes_PacienteId",
                table: "Antecedentes",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_CuidadorId",
                table: "Pacientes",
                column: "CuidadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_MadreId",
                table: "Pacientes",
                column: "MadreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Antecedentes");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
