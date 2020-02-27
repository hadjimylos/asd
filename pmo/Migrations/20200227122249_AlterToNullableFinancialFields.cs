using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AlterToNullableFinancialFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "QualCosts",
                table: "FinancialData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherDevelopmentExpenses",
                table: "FinancialData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "GPAExpense",
                table: "FinancialData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "GPACapital",
                table: "FinancialData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "QualCosts",
                table: "FinancialData",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherDevelopmentExpenses",
                table: "FinancialData",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GPAExpense",
                table: "FinancialData",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GPACapital",
                table: "FinancialData",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
