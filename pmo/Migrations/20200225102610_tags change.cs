using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class tagschange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 223,
                column: "TagCategoryId",
                value: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 223,
                column: "TagCategoryId",
                value: 4);
        }
    }
}
