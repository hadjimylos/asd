using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Mitigations");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Mitigations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Mitigations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Mitigations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
