using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class ManutencaoProgramada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManutençãoProgramadas_Manutençãos_ManutençãoID",
                table: "ManutençãoProgramadas");

            migrationBuilder.DropColumn(
                name: "Peça",
                table: "Manutençãos");

            migrationBuilder.RenameColumn(
                name: "ManutençãoID",
                table: "ManutençãoProgramadas",
                newName: "VeículoID1");

            migrationBuilder.RenameColumn(
                name: "KM",
                table: "ManutençãoProgramadas",
                newName: "DeltaKM");

            migrationBuilder.RenameIndex(
                name: "IX_ManutençãoProgramadas_ManutençãoID",
                table: "ManutençãoProgramadas",
                newName: "IX_ManutençãoProgramadas_VeículoID1");

            migrationBuilder.AddColumn<int>(
                name: "TipoVeículo",
                table: "Marcas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoManutenção",
                table: "Manutençãos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ÁreaManutenção",
                table: "Manutençãos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeltaDias",
                table: "ManutençãoProgramadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoManutenção",
                table: "ManutençãoProgramadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoVeículo",
                table: "ManutençãoProgramadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ÁreaManutenção",
                table: "ManutençãoProgramadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ManutençãoProgramadas_Veículos_VeículoID1",
                table: "ManutençãoProgramadas",
                column: "VeículoID1",
                principalTable: "Veículos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManutençãoProgramadas_Veículos_VeículoID1",
                table: "ManutençãoProgramadas");

            migrationBuilder.DropColumn(
                name: "TipoVeículo",
                table: "Marcas");

            migrationBuilder.DropColumn(
                name: "TipoManutenção",
                table: "Manutençãos");

            migrationBuilder.DropColumn(
                name: "ÁreaManutenção",
                table: "Manutençãos");

            migrationBuilder.DropColumn(
                name: "DeltaDias",
                table: "ManutençãoProgramadas");

            migrationBuilder.DropColumn(
                name: "TipoManutenção",
                table: "ManutençãoProgramadas");

            migrationBuilder.DropColumn(
                name: "TipoVeículo",
                table: "ManutençãoProgramadas");

            migrationBuilder.DropColumn(
                name: "ÁreaManutenção",
                table: "ManutençãoProgramadas");

            migrationBuilder.RenameColumn(
                name: "VeículoID1",
                table: "ManutençãoProgramadas",
                newName: "ManutençãoID");

            migrationBuilder.RenameColumn(
                name: "DeltaKM",
                table: "ManutençãoProgramadas",
                newName: "KM");

            migrationBuilder.RenameIndex(
                name: "IX_ManutençãoProgramadas_VeículoID1",
                table: "ManutençãoProgramadas",
                newName: "IX_ManutençãoProgramadas_ManutençãoID");

            migrationBuilder.AddColumn<string>(
                name: "Peça",
                table: "Manutençãos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ManutençãoProgramadas_Manutençãos_ManutençãoID",
                table: "ManutençãoProgramadas",
                column: "ManutençãoID",
                principalTable: "Manutençãos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
