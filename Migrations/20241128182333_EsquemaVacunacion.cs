using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class EsquemaVacunacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diluyentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diluyentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jeringas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jeringas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosInventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoRecurso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecursoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosInventario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sueros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrascosDisponibles = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sueros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDosis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDosis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacunas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Laboratorio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DosisDisponibles = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacunas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacunasAdministradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    VacunaId = table.Column<int>(type: "int", nullable: false),
                    LoteVacuna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JeringaId = table.Column<int>(type: "int", nullable: false),
                    LoteJeringa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiluyenteId = table.Column<int>(type: "int", nullable: true),
                    LoteDiluyente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SueroId = table.Column<int>(type: "int", nullable: true),
                    FrascosUtilizados = table.Column<int>(type: "int", nullable: true),
                    TipoDosisId = table.Column<int>(type: "int", nullable: false),
                    FechaAdministracion = table.Column<DateTime>(type: "datetime2", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosInventario");

            migrationBuilder.DropTable(
                name: "VacunasAdministradas");

            migrationBuilder.DropTable(
                name: "Diluyentes");

            migrationBuilder.DropTable(
                name: "Jeringas");

            migrationBuilder.DropTable(
                name: "Sueros");

            migrationBuilder.DropTable(
                name: "TiposDosis");

            migrationBuilder.DropTable(
                name: "Vacunas");
        }
    }
}
