using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class PeçaOutrosColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Combustívels");

            migrationBuilder.AddColumn<string>(
                name: "PeçaOutros",
                table: "Manutençãos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeçaOutros",
                table: "Manutençãos");

            migrationBuilder.CreateTable(
                name: "Combustívels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combustívels", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Combustívels",
                columns: new[] { "ID", "Nome" },
                values: new object[,]
                {
                    { 1, "Gasolina Comum" },
                    { 2, "Gasolina Aditivada" },
                    { 3, "Etanol Comum" },
                    { 4, "Etanol Aditivado" }
                });
        }
    }
}
