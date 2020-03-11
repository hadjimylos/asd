using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StageConfigs",
                keyColumn: "Id",
                keyValue: 5,
                column: "MinBusinessCases",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StageConfigs",
                keyColumn: "Id",
                keyValue: 5,
                column: "MinBusinessCases",
                value: 0);
        }
    }
}
