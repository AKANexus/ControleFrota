using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class EmManutencaoCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmManutenção",
                table: "Veículos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmManutenção",
                table: "Veículos");
        }
    }
}
