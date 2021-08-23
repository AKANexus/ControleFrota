using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class ColunaPosto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "Abastecimentos",
                newName: "ValorPorLitro");

            migrationBuilder.AddColumn<string>(
                name: "Posto",
                table: "Abastecimentos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Posto",
                table: "Abastecimentos");

            migrationBuilder.RenameColumn(
                name: "ValorPorLitro",
                table: "Abastecimentos",
                newName: "ValorTotal");
        }
    }
}
