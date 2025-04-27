using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBase13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EsquemasVacunacion_Pacientes_PacienteId",
                table: "EsquemasVacunacion");

            migrationBuilder.DropIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_EsquemasVacunacion_Pacientes_PacienteId",
                table: "EsquemasVacunacion",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
