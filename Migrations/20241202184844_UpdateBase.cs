using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EsquemasVacunacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoCarnet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Responsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegistradoPAI = table.Column<bool>(type: "bit", nullable: false),
                    MotivoNoIngreso = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EsquemasVacunacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EsquemasVacunacion_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EsquemaVacunacionDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EsquemaVacunacionId = table.Column<int>(type: "int", nullable: false),
                    VacunaId = table.Column<int>(type: "int", nullable: true),
                    SueroId = table.Column<int>(type: "int", nullable: true),
                    DiluyenteId = table.Column<int>(type: "int", nullable: true),
                    JeringaId = table.Column<int>(type: "int", nullable: true),
                    CantidadUtilizada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EsquemaVacunacionDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EsquemaVacunacionDetalles_Diluyentes_DiluyenteId",
                        column: x => x.DiluyenteId,
                        principalTable: "Diluyentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EsquemaVacunacionDetalles_EsquemasVacunacion_EsquemaVacunacionId",
                        column: x => x.EsquemaVacunacionId,
                        principalTable: "EsquemasVacunacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EsquemaVacunacionDetalles_Jeringas_JeringaId",
                        column: x => x.JeringaId,
                        principalTable: "Jeringas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EsquemaVacunacionDetalles_Sueros_SueroId",
                        column: x => x.SueroId,
                        principalTable: "Sueros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EsquemaVacunacionDetalles_Vacunas_VacunaId",
                        column: x => x.VacunaId,
                        principalTable: "Vacunas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EsquemasVacunacion_PacienteId",
                table: "EsquemasVacunacion",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaVacunacionDetalles_DiluyenteId",
                table: "EsquemaVacunacionDetalles",
                column: "DiluyenteId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaVacunacionDetalles_EsquemaVacunacionId",
                table: "EsquemaVacunacionDetalles",
                column: "EsquemaVacunacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaVacunacionDetalles_JeringaId",
                table: "EsquemaVacunacionDetalles",
                column: "JeringaId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaVacunacionDetalles_SueroId",
                table: "EsquemaVacunacionDetalles",
                column: "SueroId");

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaVacunacionDetalles_VacunaId",
                table: "EsquemaVacunacionDetalles",
                column: "VacunaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EsquemaVacunacionDetalles");

            migrationBuilder.DropTable(
                name: "EsquemasVacunacion");
        }
    }
}
