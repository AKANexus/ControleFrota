using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class StatusVeicMtrista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Veículos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Motoristas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Veículos");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Motoristas");
        }
    }
}
