using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBase10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Cuidadores_CuidadorId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Madres_MadreId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion");

            migrationBuilder.DropIndex(
                name: "IX_CondicionesUsuarias_PacienteId",
                table: "CondicionesUsuarias");

            migrationBuilder.DropIndex(
                name: "IX_AntecedentesMedicos_PacienteId",
                table: "AntecedentesMedicos");

            migrationBuilder.AlterColumn<int>(
                name: "MadreId",
                table: "Pacientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CuidadorId",
                table: "Pacientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CondicionesUsuarias_PacienteId",
                table: "CondicionesUsuarias",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedentesMedicos_PacienteId",
                table: "AntecedentesMedicos",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Cuidadores_CuidadorId",
                table: "Pacientes",
                column: "CuidadorId",
                principalTable: "Cuidadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Madres_MadreId",
                table: "Pacientes",
                column: "MadreId",
                principalTable: "Madres",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Cuidadores_CuidadorId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Madres_MadreId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion");

            migrationBuilder.DropIndex(
                name: "IX_CondicionesUsuarias_PacienteId",
                table: "CondicionesUsuarias");

            migrationBuilder.DropIndex(
                name: "IX_AntecedentesMedicos_PacienteId",
                table: "AntecedentesMedicos");

            migrationBuilder.AlterColumn<int>(
                name: "MadreId",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CuidadorId",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion",
                column: "PacienteId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Cuidadores_CuidadorId",
                table: "Pacientes",
                column: "CuidadorId",
                principalTable: "Cuidadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Madres_MadreId",
                table: "Pacientes",
                column: "MadreId",
                principalTable: "Madres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
