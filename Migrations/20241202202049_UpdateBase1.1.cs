using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBase11 : Migration
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
                name: "IX_Pacientes_CuidadorId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_MadreId",
                table: "Pacientes");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_Pacientes_CuidadorId",
                table: "Pacientes",
                column: "CuidadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_MadreId",
                table: "Pacientes",
                column: "MadreId");

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
    }
}
