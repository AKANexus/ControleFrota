using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class CombustívelSeedAndTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combustível",
                table: "Abastecimentos");

            migrationBuilder.AddColumn<int>(
                name: "CombustívelID",
                table: "Abastecimentos",
                type: "int",
                nullable: true);

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
                    { 4, "Etanol Adivitado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abastecimentos_CombustívelID",
                table: "Abastecimentos",
                column: "CombustívelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Abastecimentos_Combustívels_CombustívelID",
                table: "Abastecimentos",
                column: "CombustívelID",
                principalTable: "Combustívels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abastecimentos_Combustívels_CombustívelID",
                table: "Abastecimentos");

            migrationBuilder.DropTable(
                name: "Combustívels");

            migrationBuilder.DropIndex(
                name: "IX_Abastecimentos_CombustívelID",
                table: "Abastecimentos");

            migrationBuilder.DropColumn(
                name: "CombustívelID",
                table: "Abastecimentos");

            migrationBuilder.AddColumn<int>(
                name: "Combustível",
                table: "Abastecimentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
