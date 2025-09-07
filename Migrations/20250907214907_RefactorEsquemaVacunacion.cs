using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEsquemaVacunacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAplicacion",
                table: "EsquemaVacunacionDetalles");

            migrationBuilder.DropColumn(
                name: "NumeroDosis",
                table: "EsquemaVacunacionDetalles");

            migrationBuilder.DropColumn(
                name: "DiasIntervalo",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "FrecuenciaAplicacion",
                table: "EsquemasVacunacion");

            migrationBuilder.RenameColumn(
                name: "MotivoNoIngreso",
                table: "EsquemasVacunacion",
                newName: "MotivoIngreso");

            migrationBuilder.RenameColumn(
                name: "FechaPrimeraDosis",
                table: "EsquemasVacunacion",
                newName: "FechaDosisAplicada");

            migrationBuilder.RenameColumn(
                name: "CantidadTotalDosis",
                table: "EsquemasVacunacion",
                newName: "NumeroDeDosis");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaProximaDosis",
                table: "EsquemasVacunacion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lote",
                table: "EsquemasVacunacion",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SitioDeAplicacion",
                table: "EsquemasVacunacion",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViaDeAdministracion",
                table: "EsquemasVacunacion",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaProximaDosis",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "Lote",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "SitioDeAplicacion",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "ViaDeAdministracion",
                table: "EsquemasVacunacion");

            migrationBuilder.RenameColumn(
                name: "NumeroDeDosis",
                table: "EsquemasVacunacion",
                newName: "CantidadTotalDosis");

            migrationBuilder.RenameColumn(
                name: "MotivoIngreso",
                table: "EsquemasVacunacion",
                newName: "MotivoNoIngreso");

            migrationBuilder.RenameColumn(
                name: "FechaDosisAplicada",
                table: "EsquemasVacunacion",
                newName: "FechaPrimeraDosis");

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

            migrationBuilder.AddColumn<int>(
                name: "DiasIntervalo",
                table: "EsquemasVacunacion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrecuenciaAplicacion",
                table: "EsquemasVacunacion",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
