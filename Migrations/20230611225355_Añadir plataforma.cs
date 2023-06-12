using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parcial1.Migrations
{
    /// <inheritdoc />
    public partial class Añadirplataforma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plataforma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Empresa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataforma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlataformaVideojuego",
                columns: table => new
                {
                    PlataformasId = table.Column<int>(type: "INTEGER", nullable: false),
                    VideojuegosId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlataformaVideojuego", x => new { x.PlataformasId, x.VideojuegosId });
                    table.ForeignKey(
                        name: "FK_PlataformaVideojuego_Plataforma_PlataformasId",
                        column: x => x.PlataformasId,
                        principalTable: "Plataforma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlataformaVideojuego_Videojuego_VideojuegosId",
                        column: x => x.VideojuegosId,
                        principalTable: "Videojuego",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlataformaVideojuego_VideojuegosId",
                table: "PlataformaVideojuego",
                column: "VideojuegosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlataformaVideojuego");

            migrationBuilder.DropTable(
                name: "Plataforma");
        }
    }
}
