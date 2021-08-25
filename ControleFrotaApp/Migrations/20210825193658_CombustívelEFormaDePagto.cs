using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class CombustívelEFormaDePagto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abastecimentos_Combustívels_CombustívelID",
                table: "Abastecimentos");

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

            migrationBuilder.AddColumn<int>(
                name: "FormasPagamento",
                table: "Abastecimentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combustível",
                table: "Abastecimentos");

            migrationBuilder.DropColumn(
                name: "FormasPagamento",
                table: "Abastecimentos");

            migrationBuilder.AddColumn<int>(
                name: "CombustívelID",
                table: "Abastecimentos",
                type: "int",
                nullable: true);

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
    }
}
