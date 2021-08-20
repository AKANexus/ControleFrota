using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class InitialD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Motoristas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNH = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motoristas", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Veículos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Placa = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MarcaID = table.Column<int>(type: "int", nullable: true),
                    ModeloID = table.Column<int>(type: "int", nullable: true),
                    RENAVAM = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ÚltimoLicenciamento = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veículos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Veículos_Marcas_MarcaID",
                        column: x => x.MarcaID,
                        principalTable: "Marcas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Veículos_Modelos_ModeloID",
                        column: x => x.ModeloID,
                        principalTable: "Modelos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Abastecimentos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KM = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Combustível = table.Column<int>(type: "int", nullable: false),
                    MotoristaID = table.Column<int>(type: "int", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Litragem = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VeículoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abastecimentos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Abastecimentos_Motoristas_MotoristaID",
                        column: x => x.MotoristaID,
                        principalTable: "Motoristas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Abastecimentos_Veículos_VeículoID",
                        column: x => x.VeículoID,
                        principalTable: "Veículos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Manutençãos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Peça = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Motivo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KM = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Preço = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Local = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VeículoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manutençãos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Manutençãos_Veículos_VeículoID",
                        column: x => x.VeículoID,
                        principalTable: "Veículos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Viagems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MotoristaID = table.Column<int>(type: "int", nullable: true),
                    Motivo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KMInicial = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    KMFinal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Saída = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Retorno = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VeículoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viagems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Viagems_Motoristas_MotoristaID",
                        column: x => x.MotoristaID,
                        principalTable: "Motoristas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Viagems_Veículos_VeículoID",
                        column: x => x.VeículoID,
                        principalTable: "Veículos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ManutençãoProgramadas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VeículoID = table.Column<int>(type: "int", nullable: true),
                    ManutençãoID = table.Column<int>(type: "int", nullable: true),
                    KM = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManutençãoProgramadas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ManutençãoProgramadas_Manutençãos_ManutençãoID",
                        column: x => x.ManutençãoID,
                        principalTable: "Manutençãos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManutençãoProgramadas_Veículos_VeículoID",
                        column: x => x.VeículoID,
                        principalTable: "Veículos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    { 6, "Yamaha" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abastecimentos_MotoristaID",
                table: "Abastecimentos",
                column: "MotoristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Abastecimentos_VeículoID",
                table: "Abastecimentos",
                column: "VeículoID");

            migrationBuilder.CreateIndex(
                name: "IX_ManutençãoProgramadas_ManutençãoID",
                table: "ManutençãoProgramadas",
                column: "ManutençãoID");

            migrationBuilder.CreateIndex(
                name: "IX_ManutençãoProgramadas_VeículoID",
                table: "ManutençãoProgramadas",
                column: "VeículoID");

            migrationBuilder.CreateIndex(
                name: "IX_Manutençãos_VeículoID",
                table: "Manutençãos",
                column: "VeículoID");

            migrationBuilder.CreateIndex(
                name: "IX_Veículos_MarcaID",
                table: "Veículos",
                column: "MarcaID");

            migrationBuilder.CreateIndex(
                name: "IX_Veículos_ModeloID",
                table: "Veículos",
                column: "ModeloID");

            migrationBuilder.CreateIndex(
                name: "IX_Viagems_MotoristaID",
                table: "Viagems",
                column: "MotoristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Viagems_VeículoID",
                table: "Viagems",
                column: "VeículoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abastecimentos");

            migrationBuilder.DropTable(
                name: "ManutençãoProgramadas");

            migrationBuilder.DropTable(
                name: "Viagems");

            migrationBuilder.DropTable(
                name: "Manutençãos");

            migrationBuilder.DropTable(
                name: "Motoristas");

            migrationBuilder.DropTable(
                name: "Veículos");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Modelos");
        }
    }
}
