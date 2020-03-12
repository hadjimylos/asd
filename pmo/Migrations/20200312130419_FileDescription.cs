using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class FileDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OptionalFiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "OptionalFiles");
        }
    }
}
