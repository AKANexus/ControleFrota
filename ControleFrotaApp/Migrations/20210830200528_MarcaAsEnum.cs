using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class MarcaAsEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modelos_Marcas_MarcaID",
                table: "Modelos");

            migrationBuilder.DropForeignKey(
                name: "FK_Veículos_Marcas_MarcaID",
                table: "Veículos");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropIndex(
                name: "IX_Veículos_MarcaID",
                table: "Veículos");

            migrationBuilder.DropIndex(
                name: "IX_Modelos_MarcaID",
                table: "Modelos");

            migrationBuilder.DropColumn(
                name: "MarcaID",
                table: "Veículos");

            migrationBuilder.DropColumn(
                name: "MarcaID",
                table: "Modelos");

            migrationBuilder.AddColumn<int>(
                name: "Marca",
                table: "Modelos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Modelos");

            migrationBuilder.AddColumn<int>(
                name: "MarcaID",
                table: "Veículos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcaID",
                table: "Modelos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "ID", "Nome" },
                values: new object[,]
                {
                    { 1, "Fiat" },
                    { 2, "Ford" },
                    { 3, "Jeep" },
                    { 4, "Renault" },
                    { 5, "Volkswagen" },
                    { 6, "Yamaha" },
                    { 7, "Honda" },
                    { 8, "Suzuki" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veículos_MarcaID",
                table: "Veículos",
                column: "MarcaID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Veículos_Marcas_MarcaID",
                table: "Veículos",
                column: "MarcaID",
                principalTable: "Marcas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
