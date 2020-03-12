using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddCheckboxField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMarketingRequired",
                table: "ProductIntroChecklists");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "ProductIntroChecklists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "CustomerDesignApprovals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "ProductIntroChecklists");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "CustomerDesignApprovals");

            migrationBuilder.AddColumn<bool>(
                name: "IsMarketingRequired",
                table: "ProductIntroChecklists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
