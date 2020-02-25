using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class FixBusinessCaseColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataStartingDate",
                table: "BusinessCases");

            migrationBuilder.DropColumn(
                name: "NumberOfYears",
                table: "BusinessCases");

            migrationBuilder.AddColumn<int>(
                name: "FinancialStartYear",
                table: "BusinessCases",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinancialStartYear",
                table: "BusinessCases");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataStartingDate",
                table: "BusinessCases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfYears",
                table: "BusinessCases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
