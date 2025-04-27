using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBase12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CondicionesUsuarias_PacienteId",
                table: "CondicionesUsuarias");

            migrationBuilder.DropIndex(
                name: "IX_AntecedentesMedicos_PacienteId",
                table: "AntecedentesMedicos");

            migrationBuilder.CreateIndex(
                name: "IX_CondicionesUsuarias_PacienteId",
                table: "CondicionesUsuarias",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AntecedentesMedicos_PacienteId",
                table: "AntecedentesMedicos",
                column: "PacienteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CondicionesUsuarias_PacienteId",
                table: "CondicionesUsuarias");

            migrationBuilder.DropIndex(
                name: "IX_AntecedentesMedicos_PacienteId",
                table: "AntecedentesMedicos");

            migrationBuilder.CreateIndex(
                name: "IX_CondicionesUsuarias_PacienteId",
                table: "CondicionesUsuarias",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedentesMedicos_PacienteId",
                table: "AntecedentesMedicos",
                column: "PacienteId");
        }
    }
}
