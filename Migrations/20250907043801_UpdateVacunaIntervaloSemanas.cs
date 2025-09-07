using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVacunaIntervaloSemanas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertasVacunacion");

            migrationBuilder.RenameColumn(
                name: "IntervaloMeses",
                table: "Vacunas",
                newName: "IntervaloSemanas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IntervaloSemanas",
                table: "Vacunas",
                newName: "IntervaloMeses");

            migrationBuilder.CreateTable(
                name: "AlertasVacunacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    VacunaId = table.Column<int>(type: "int", nullable: false),
                    CedulaPaciente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaNotificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaProximaDosis = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NombreVacuna = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroDosisProxima = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ok = table.Column<bool>(type: "bit", nullable: false),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_AlertasVacunacion_PacienteId",
                table: "AlertasVacunacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasVacunacion_VacunaId",
                table: "AlertasVacunacion",
                column: "VacunaId");
        }
    }
}
