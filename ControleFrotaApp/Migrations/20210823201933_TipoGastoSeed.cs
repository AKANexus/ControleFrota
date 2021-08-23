using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class TipoGastoSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbastecimentoID",
                table: "Gastos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Combustívels",
                keyColumn: "ID",
                keyValue: 4,
                column: "Nome",
                value: "Etanol Aditivado");

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "ID", "Nome" },
                values: new object[,]
                {
                    { 7, "Honda" },
                    { 8, "Suzuki" }
                });

            migrationBuilder.InsertData(
                table: "TipoGastos",
                columns: new[] { "ID", "Descrição" },
                values: new object[,]
                {
                    { 1, "Pedágio" },
                    { 2, "Hospedagem" },
                    { 3, "Alimentação" },
                    { 4, "Abastecimento*" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_AbastecimentoID",
                table: "Gastos",
                column: "AbastecimentoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Abastecimentos_AbastecimentoID",
                table: "Gastos",
                column: "AbastecimentoID",
                principalTable: "Abastecimentos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Abastecimentos_AbastecimentoID",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_AbastecimentoID",
                table: "Gastos");

            migrationBuilder.DeleteData(
                table: "Marcas",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Marcas",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TipoGastos",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoGastos",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoGastos",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoGastos",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "AbastecimentoID",
                table: "Gastos");

            migrationBuilder.UpdateData(
                table: "Combustívels",
                keyColumn: "ID",
                keyValue: 4,
                column: "Nome",
                value: "Etanol Adivitado");
        }
    }
}
