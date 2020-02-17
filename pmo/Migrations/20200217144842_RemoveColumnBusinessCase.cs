using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class RemoveColumnBusinessCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkRequirementUrl",
                table: "BusinessCases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkRequirementUrl",
                table: "BusinessCases",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
