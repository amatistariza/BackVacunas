using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddMadres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosInventario");

            migrationBuilder.DropTable(
                name: "VacunasAdministradas");

            migrationBuilder.DropTable(
                name: "TiposDosis");

            migrationBuilder.CreateTable(
                name: "Madres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndicativoTelefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TelefonoFijo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    RegimenAfiliacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PertenenciaEtnica = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Desplazado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Madres", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Madres");

            migrationBuilder.CreateTable(
                name: "MovimientosInventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecursoId = table.Column<int>(type: "int", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoRecurso = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosInventario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDosis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDosis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacunasAdministradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiluyenteId = table.Column<int>(type: "int", nullable: true),
                    JeringaId = table.Column<int>(type: "int", nullable: false),
                    SueroId = table.Column<int>(type: "int", nullable: true),
                    TipoDosisId = table.Column<int>(type: "int", nullable: false),
                    VacunaId = table.Column<int>(type: "int", nullable: false),
                    FechaAdministracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrascosUtilizados = table.Column<int>(type: "int", nullable: true),
                    LoteDiluyente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoteJeringa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoteVacuna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacunasAdministradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacunasAdministradas_Diluyentes_DiluyenteId",
                        column: x => x.DiluyenteId,
                        principalTable: "Diluyentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VacunasAdministradas_Jeringas_JeringaId",
                        column: x => x.JeringaId,
                        principalTable: "Jeringas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacunasAdministradas_Sueros_SueroId",
                        column: x => x.SueroId,
                        principalTable: "Sueros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VacunasAdministradas_TiposDosis_TipoDosisId",
                        column: x => x.TipoDosisId,
                        principalTable: "TiposDosis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacunasAdministradas_Vacunas_VacunaId",
                        column: x => x.VacunaId,
                        principalTable: "Vacunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacunasAdministradas_DiluyenteId",
                table: "VacunasAdministradas",
                column: "DiluyenteId");

            migrationBuilder.CreateIndex(
                name: "IX_VacunasAdministradas_JeringaId",
                table: "VacunasAdministradas",
                column: "JeringaId");

            migrationBuilder.CreateIndex(
                name: "IX_VacunasAdministradas_SueroId",
                table: "VacunasAdministradas",
                column: "SueroId");

            migrationBuilder.CreateIndex(
                name: "IX_VacunasAdministradas_TipoDosisId",
                table: "VacunasAdministradas",
                column: "TipoDosisId");

            migrationBuilder.CreateIndex(
                name: "IX_VacunasAdministradas_VacunaId",
                table: "VacunasAdministradas",
                column: "VacunaId");
        }
    }
}
