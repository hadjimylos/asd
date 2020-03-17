using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddIsLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocation",
                table: "StageFiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocation",
                table: "StageFileConfigs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocation",
                table: "LiteStageFileConfigs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocation",
                table: "StageFiles");

            migrationBuilder.DropColumn(
                name: "IsLocation",
                table: "StageFileConfigs");

            migrationBuilder.DropColumn(
                name: "IsLocation",
                table: "LiteStageFileConfigs");
        }
    }
}
