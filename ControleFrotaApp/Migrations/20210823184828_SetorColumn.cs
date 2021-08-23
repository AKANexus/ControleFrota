using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class SetorColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SetorID",
                table: "Motoristas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Setors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descrição = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setors", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Setors",
                columns: new[] { "ID", "Descrição" },
                values: new object[,]
                {
                    { 1, "Infraestrutura" },
                    { 2, "Suporte Técnico" },
                    { 3, "Vendas" },
                    { 4, "Administração" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motoristas_SetorID",
                table: "Motoristas",
                column: "SetorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Motoristas_Setors_SetorID",
                table: "Motoristas",
                column: "SetorID",
                principalTable: "Setors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motoristas_Setors_SetorID",
                table: "Motoristas");

            migrationBuilder.DropTable(
                name: "Setors");

            migrationBuilder.DropIndex(
                name: "IX_Motoristas_SetorID",
                table: "Motoristas");

            migrationBuilder.DropColumn(
                name: "SetorID",
                table: "Motoristas");
        }
    }
}
