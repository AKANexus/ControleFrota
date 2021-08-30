using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class ManutencaoProgramadaCarros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ManutençãoProgramadas",
                columns: new[] { "ID", "DeltaDias", "DeltaKM", "TipoManutenção", "TipoVeículo", "VeículoID", "VeículoID1", "ÁreaManutenção" },
                values: new object[,]
                {
                    { 1, 0, 50000m, 1, 0, null, null, 0 },
                    { 30, 0, 10000m, 0, 0, null, null, 20 },
                    { 29, 365, 0m, 1, 0, null, null, 20 },
                    { 28, 0, 20000m, 1, 0, null, null, 19 },
                    { 27, 0, 5000m, 0, 0, null, null, 18 },
                    { 26, 0, 7500m, 1, 0, null, null, 18 },
                    { 25, 0, 5000m, 0, 0, null, null, 17 },
                    { 24, 0, 7500m, 1, 0, null, null, 17 },
                    { 23, 30, 0m, 0, 0, null, null, 16 },
                    { 22, 0, 10000m, 0, 0, null, null, 15 },
                    { 21, 365, 0m, 1, 0, null, null, 15 },
                    { 20, 0, 80000m, 1, 0, null, null, 14 },
                    { 19, 730, 0m, 1, 0, null, null, 13 },
                    { 18, 0, 10000m, 0, 0, null, null, 12 },
                    { 17, 0, 10000m, 0, 0, null, null, 11 },
                    { 16, 0, 50000m, 1, 0, null, null, 11 },
                    { 15, 0, 5000m, 0, 0, null, null, 10 },
                    { 14, 730, 0m, 1, 0, null, null, 10 },
                    { 13, 0, 10000m, 0, 0, null, null, 9 },
                    { 12, 0, 10000m, 0, 0, null, null, 8 },
                    { 11, 0, 100000m, 1, 0, null, null, 8 },
                    { 10, 0, 5000m, 0, 0, null, null, 7 },
                    { 9, 0, 5000m, 0, 0, null, null, 6 },
                    { 8, 0, 40000m, 1, 0, null, null, 6 },
                    { 6, 0, 100000m, 1, 0, null, null, 5 },
                    { 7, 0, 10000m, 0, 0, null, null, 5 },
                    { 5, 2555, 0m, 1, 0, null, null, 4 },
                    { 4, 0, 10000m, 0, 0, null, null, 2 },
                    { 3, 0, 10000m, 0, 0, null, null, 1 },
                    { 2, 0, 10000m, 0, 0, null, null, 0 },
                    { 31, 60, 0m, 0, 0, null, null, 21 },
                    { 32, 30, 0m, 0, 0, null, null, 22 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ManutençãoProgramadas",
                keyColumn: "ID",
                keyValue: 32);
        }
    }
}
