using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarSistemaAlertasVacunacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadTotalDosis",
                table: "EsquemasVacunacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiasIntervalo",
                table: "EsquemasVacunacion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPrimeraDosis",
                table: "EsquemasVacunacion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FrecuenciaAplicacion",
                table: "EsquemasVacunacion",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VacunaId",
                table: "EsquemasVacunacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AlertasVacunacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    CedulaPaciente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VacunaId = table.Column<int>(type: "int", nullable: false),
                    NombreVacuna = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroDosisProxima = table.Column<int>(type: "int", nullable: false),
                    FechaProximaDosis = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ok = table.Column<bool>(type: "bit", nullable: false),
                    FechaNotificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertasVacunacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertasVacunacion_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlertasVacunacion_Vacunas_VacunaId",
                        column: x => x.VacunaId,
                        principalTable: "Vacunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemasVacunacion_VacunaId",
                table: "EsquemasVacunacion",
                column: "VacunaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasVacunacion_PacienteId",
                table: "AlertasVacunacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasVacunacion_VacunaId",
                table: "AlertasVacunacion",
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

            migrationBuilder.AddForeignKey(
                name: "FK_EsquemasVacunacion_Pacientes_PacienteId",
                table: "EsquemasVacunacion",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EsquemasVacunacion_Vacunas_VacunaId",
                table: "EsquemasVacunacion",
                column: "VacunaId",
                principalTable: "Vacunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EsquemasVacunacion_Pacientes_PacienteId",
                table: "EsquemasVacunacion");

            migrationBuilder.DropForeignKey(
                name: "FK_EsquemasVacunacion_Vacunas_VacunaId",
                table: "EsquemasVacunacion");

            migrationBuilder.DropTable(
                name: "AlertasVacunacion");

            migrationBuilder.DropTable(
                name: "RegistrosVacunacion");

            migrationBuilder.DropIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion");

            migrationBuilder.DropIndex(
                name: "IX_EsquemasVacunacion_VacunaId",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "CantidadTotalDosis",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "DiasIntervalo",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "FechaPrimeraDosis",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "FrecuenciaAplicacion",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "VacunaId",
                table: "EsquemasVacunacion");
        }
    }
}
