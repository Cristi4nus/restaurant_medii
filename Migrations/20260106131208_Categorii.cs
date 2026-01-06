using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurant_medii.Migrations
{
    /// <inheritdoc />
    public partial class Categorii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alergeni",
                table: "Produs");

            migrationBuilder.AddColumn<int>(
                name: "CategorieID",
                table: "Produs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produs_CategorieID",
                table: "Produs",
                column: "CategorieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Categorie_CategorieID",
                table: "Produs",
                column: "CategorieID",
                principalTable: "Categorie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Categorie_CategorieID",
                table: "Produs");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropIndex(
                name: "IX_Produs_CategorieID",
                table: "Produs");

            migrationBuilder.DropColumn(
                name: "CategorieID",
                table: "Produs");

            migrationBuilder.AddColumn<string>(
                name: "Alergeni",
                table: "Produs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
