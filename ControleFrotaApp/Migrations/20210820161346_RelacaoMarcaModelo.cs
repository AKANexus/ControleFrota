using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class RelacaoMarcaModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcaID",
                table: "Modelos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaID",
                table: "Modelos",
                column: "MarcaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Modelos_Marcas_MarcaID",
                table: "Modelos",
                column: "MarcaID",
                principalTable: "Marcas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modelos_Marcas_MarcaID",
                table: "Modelos");

            migrationBuilder.DropIndex(
                name: "IX_Modelos_MarcaID",
                table: "Modelos");

            migrationBuilder.DropColumn(
                name: "MarcaID",
                table: "Modelos");
        }
    }
}
