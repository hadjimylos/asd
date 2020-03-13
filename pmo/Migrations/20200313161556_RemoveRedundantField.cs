using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class RemoveRedundantField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainsInfingmentIssues",
                table: "ProductInfrigmentPatentabilities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ContainsInfingmentIssues",
                table: "ProductInfrigmentPatentabilities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
