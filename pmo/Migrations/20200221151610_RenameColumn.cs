using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class RenameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesCostEstimated",
                table: "FinancialData");

            migrationBuilder.AddColumn<decimal>(
                name: "SalesPriceEstimated",
                table: "FinancialData",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesPriceEstimated",
                table: "FinancialData");

            migrationBuilder.AddColumn<decimal>(
                name: "SalesCostEstimated",
                table: "FinancialData",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
