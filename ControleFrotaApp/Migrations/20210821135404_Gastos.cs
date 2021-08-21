using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class Gastos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_TipoGasto_TipoGastoID",
                table: "Gasto");

            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Viagems_ViagemID",
                table: "Gasto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoGasto",
                table: "TipoGasto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gasto",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "Decimal",
                table: "Viagems");

            migrationBuilder.RenameTable(
                name: "TipoGasto",
                newName: "TipoGastos");

            migrationBuilder.RenameTable(
                name: "Gasto",
                newName: "Gastos");

            migrationBuilder.RenameIndex(
                name: "IX_Gasto_ViagemID",
                table: "Gastos",
                newName: "IX_Gastos_ViagemID");

            migrationBuilder.RenameIndex(
                name: "IX_Gasto_TipoGastoID",
                table: "Gastos",
                newName: "IX_Gastos_TipoGastoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoGastos",
                table: "TipoGastos",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gastos",
                table: "Gastos",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_TipoGastos_TipoGastoID",
                table: "Gastos",
                column: "TipoGastoID",
                principalTable: "TipoGastos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Viagems_ViagemID",
                table: "Gastos",
                column: "ViagemID",
                principalTable: "Viagems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_TipoGastos_TipoGastoID",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Viagems_ViagemID",
                table: "Gastos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoGastos",
                table: "TipoGastos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gastos",
                table: "Gastos");

            migrationBuilder.RenameTable(
                name: "TipoGastos",
                newName: "TipoGasto");

            migrationBuilder.RenameTable(
                name: "Gastos",
                newName: "Gasto");

            migrationBuilder.RenameIndex(
                name: "IX_Gastos_ViagemID",
                table: "Gasto",
                newName: "IX_Gasto_ViagemID");

            migrationBuilder.RenameIndex(
                name: "IX_Gastos_TipoGastoID",
                table: "Gasto",
                newName: "IX_Gasto_TipoGastoID");

            migrationBuilder.AddColumn<decimal>(
                name: "Decimal",
                table: "Viagems",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoGasto",
                table: "TipoGasto",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gasto",
                table: "Gasto",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_TipoGasto_TipoGastoID",
                table: "Gasto",
                column: "TipoGastoID",
                principalTable: "TipoGasto",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Viagems_ViagemID",
                table: "Gasto",
                column: "ViagemID",
                principalTable: "Viagems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
