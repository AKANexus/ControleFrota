using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class ManutencaoObsCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observações",
                table: "Manutençãos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observações",
                table: "Manutençãos");
        }
    }
}
