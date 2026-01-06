using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurant_medii.Migrations
{
    /// <inheritdoc />
    public partial class Alergeni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlergenID",
                table: "Categorie",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Alergen",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeAlergen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alergen", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AlergenProdus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdusID = table.Column<int>(type: "int", nullable: false),
                    AlergenID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlergenProdus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlergenProdus_Alergen_AlergenID",
                        column: x => x.AlergenID,
                        principalTable: "Alergen",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlergenProdus_Produs_ProdusID",
                        column: x => x.ProdusID,
                        principalTable: "Produs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorie_AlergenID",
                table: "Categorie",
                column: "AlergenID");

            migrationBuilder.CreateIndex(
                name: "IX_AlergenProdus_AlergenID",
                table: "AlergenProdus",
                column: "AlergenID");

            migrationBuilder.CreateIndex(
                name: "IX_AlergenProdus_ProdusID",
                table: "AlergenProdus",
                column: "ProdusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorie_Alergen_AlergenID",
                table: "Categorie",
                column: "AlergenID",
                principalTable: "Alergen",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorie_Alergen_AlergenID",
                table: "Categorie");

            migrationBuilder.DropTable(
                name: "AlergenProdus");

            migrationBuilder.DropTable(
                name: "Alergen");

            migrationBuilder.DropIndex(
                name: "IX_Categorie_AlergenID",
                table: "Categorie");

            migrationBuilder.DropColumn(
                name: "AlergenID",
                table: "Categorie");
        }
    }
}
