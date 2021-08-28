using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class ManutençãoColunas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "Manutençãos");

            migrationBuilder.AddColumn<int>(
                name: "MotoristaID",
                table: "Manutençãos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoReparo",
                table: "Manutençãos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Manutençãos_MotoristaID",
                table: "Manutençãos",
                column: "MotoristaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Manutençãos_Motoristas_MotoristaID",
                table: "Manutençãos",
                column: "MotoristaID",
                principalTable: "Motoristas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manutençãos_Motoristas_MotoristaID",
                table: "Manutençãos");

            migrationBuilder.DropIndex(
                name: "IX_Manutençãos_MotoristaID",
                table: "Manutençãos");

            migrationBuilder.DropColumn(
                name: "MotoristaID",
                table: "Manutençãos");

            migrationBuilder.DropColumn(
                name: "TipoReparo",
                table: "Manutençãos");

            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "Manutençãos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
