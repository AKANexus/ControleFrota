﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFrota.Migrations
{
    public partial class ChassisColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chassis",
                table: "Veículos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chassis",
                table: "Veículos");
        }
    }
}
