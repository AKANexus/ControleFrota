using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class MarcaTipoModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoVeículo",
                table: "Marcas");

            migrationBuilder.AddColumn<int>(
                name: "TipoVeículo",
                table: "Modelos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoVeículo",
                table: "Modelos");

            migrationBuilder.AddColumn<int>(
                name: "TipoVeículo",
                table: "Marcas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
