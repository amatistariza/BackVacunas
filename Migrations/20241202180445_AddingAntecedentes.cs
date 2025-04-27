using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddingAntecedentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntecedentesMedicos");

            migrationBuilder.DropTable(
                name: "CondicionesUsuarias");
        }
    }
}
