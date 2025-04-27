using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarAlarmasVacunacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervaloMeses",
                table: "Vacunas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroDosis",
                table: "Vacunas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAplicacion",
                table: "EsquemaVacunacionDetalles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumeroDosis",
                table: "EsquemaVacunacionDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmasVacunacion");

            migrationBuilder.DropColumn(
                name: "IntervaloMeses",
                table: "Vacunas");

            migrationBuilder.DropColumn(
                name: "NumeroDosis",
                table: "Vacunas");

            migrationBuilder.DropColumn(
                name: "FechaAplicacion",
                table: "EsquemaVacunacionDetalles");

            migrationBuilder.DropColumn(
                name: "NumeroDosis",
                table: "EsquemaVacunacionDetalles");
        }
    }
}
