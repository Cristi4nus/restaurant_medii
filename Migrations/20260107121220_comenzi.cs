using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurant_medii.Migrations
{
    /// <inheritdoc />
    public partial class comenzi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorie_Alergen_AlergenID",
                table: "Categorie");

            migrationBuilder.DropIndex(
                name: "IX_Categorie_AlergenID",
                table: "Categorie");

            migrationBuilder.DropColumn(
                name: "AlergenID",
                table: "Categorie");

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: true),
                    ProdusID = table.Column<int>(type: "int", nullable: true),
                    DataComenzii = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comanda_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Comanda_Produs_ProdusID",
                        column: x => x.ProdusID,
                        principalTable: "Produs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_ClientID",
                table: "Comanda",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_ProdusID",
                table: "Comanda",
                column: "ProdusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.AddColumn<int>(
                name: "AlergenID",
                table: "Categorie",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorie_AlergenID",
                table: "Categorie",
                column: "AlergenID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorie_Alergen_AlergenID",
                table: "Categorie",
                column: "AlergenID",
                principalTable: "Alergen",
                principalColumn: "ID");
        }
    }
}
