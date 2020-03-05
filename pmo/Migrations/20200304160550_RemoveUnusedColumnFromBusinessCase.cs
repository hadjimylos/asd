using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class RemoveUnusedColumnFromBusinessCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultipleFieldsGeneratedTable",
                table: "BusinessCases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MultipleFieldsGeneratedTable",
                table: "BusinessCases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
