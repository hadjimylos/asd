using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class FinancialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LaborRate",
                table: "BusinessCases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "FinancialData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    BusinessCaseId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    StdCostEstimated = table.Column<decimal>(nullable: false),
                    SalesCostEstimated = table.Column<decimal>(nullable: false),
                    GPACapital = table.Column<decimal>(nullable: false),
                    GPAExpense = table.Column<decimal>(nullable: false),
                    QualCosts = table.Column<decimal>(nullable: false),
                    OtherDevelopmentExpenses = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialData_BusinessCases_BusinessCaseId",
                        column: x => x.BusinessCaseId,
                        principalTable: "BusinessCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialData_BusinessCaseId",
                table: "FinancialData",
                column: "BusinessCaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialData");

            migrationBuilder.DropColumn(
                name: "LaborRate",
                table: "BusinessCases");
        }
    }
}
