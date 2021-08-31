using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class modeloMantsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManutençãoProgramadas_Veículos_VeículoID",
                table: "ManutençãoProgramadas");

            migrationBuilder.DropForeignKey(
                name: "FK_ManutençãoProgramadas_Veículos_VeículoID1",
                table: "ManutençãoProgramadas");

            migrationBuilder.DropIndex(
                name: "IX_ManutençãoProgramadas_VeículoID",
                table: "ManutençãoProgramadas");

            migrationBuilder.DropIndex(
                name: "IX_ManutençãoProgramadas_VeículoID1",
                table: "ManutençãoProgramadas");

            migrationBuilder.DeleteData(
                table: "TipoGastos",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "VeículoID",
                table: "ManutençãoProgramadas");

            migrationBuilder.DropColumn(
                name: "VeículoID1",
                table: "ManutençãoProgramadas");

            migrationBuilder.CreateTable(
                name: "ModeloManutenção",
                columns: table => new
                {
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    ManutençãoProgramadaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloManutenção", x => new { x.ModeloId, x.ManutençãoProgramadaId });
                    table.ForeignKey(
                        name: "FK_ModeloManutenção_ManutençãoProgramadas_ManutençãoProgramadaId",
                        column: x => x.ManutençãoProgramadaId,
                        principalTable: "ManutençãoProgramadas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloManutenção_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloManutenção_ManutençãoProgramadaId",
                table: "ModeloManutenção",
                column: "ManutençãoProgramadaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModeloManutenção");

            migrationBuilder.AddColumn<int>(
                name: "VeículoID",
                table: "ManutençãoProgramadas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VeículoID1",
                table: "ManutençãoProgramadas",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "TipoGastos",
                columns: new[] { "ID", "Descrição" },
                values: new object[] { 4, "Abastecimento*" });

            migrationBuilder.CreateIndex(
                name: "IX_ManutençãoProgramadas_VeículoID",
                table: "ManutençãoProgramadas",
                column: "VeículoID");

            migrationBuilder.CreateIndex(
                name: "IX_ManutençãoProgramadas_VeículoID1",
                table: "ManutençãoProgramadas",
                column: "VeículoID1");

            migrationBuilder.AddForeignKey(
                name: "FK_ManutençãoProgramadas_Veículos_VeículoID",
                table: "ManutençãoProgramadas",
                column: "VeículoID",
                principalTable: "Veículos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManutençãoProgramadas_Veículos_VeículoID1",
                table: "ManutençãoProgramadas",
                column: "VeículoID1",
                principalTable: "Veículos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
