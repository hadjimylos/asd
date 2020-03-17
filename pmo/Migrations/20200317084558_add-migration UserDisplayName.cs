using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class addmigrationUserDisplayName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExportRestrictedUsers",
                table: "ProjectDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "ExportRestrictedUsers",
                table: "ProjectDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
