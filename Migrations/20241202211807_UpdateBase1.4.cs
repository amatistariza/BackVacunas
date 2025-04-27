using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBase14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadUtilizada",
                table: "EsquemaVacunacionDetalles");

            migrationBuilder.AddColumn<int>(
                name: "CantidadUtilizadaDiluyente",
                table: "EsquemaVacunacionDetalles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadUtilizadaJeringa",
                table: "EsquemaVacunacionDetalles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadUtilizadaSuero",
                table: "EsquemaVacunacionDetalles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadUtilizadaVacuna",
                table: "EsquemaVacunacionDetalles",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadUtilizadaDiluyente",
                table: "EsquemaVacunacionDetalles");

            migrationBuilder.DropColumn(
                name: "CantidadUtilizadaJeringa",
                table: "EsquemaVacunacionDetalles");

            migrationBuilder.DropColumn(
                name: "CantidadUtilizadaSuero",
                table: "EsquemaVacunacionDetalles");

            migrationBuilder.DropColumn(
                name: "CantidadUtilizadaVacuna",
                table: "EsquemaVacunacionDetalles");

            migrationBuilder.AddColumn<int>(
                name: "CantidadUtilizada",
                table: "EsquemaVacunacionDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
