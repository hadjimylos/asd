using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class UpdateConfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinDesignConcepts",
                table: "StageConfigs");

            migrationBuilder.DropColumn(
                name: "MinQualificationTesting",
                table: "StageConfigs");

            migrationBuilder.DropColumn(
                name: "MinRampResourcePlans",
                table: "StageConfigs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinDesignConcepts",
                table: "StageConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinQualificationTesting",
                table: "StageConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinRampResourcePlans",
                table: "StageConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "StageConfigs",
                keyColumn: "Id",
                keyValue: 2,
                column: "MinDesignConcepts",
                value: 1);

            migrationBuilder.UpdateData(
                table: "StageConfigs",
                keyColumn: "Id",
                keyValue: 3,
                column: "MinRampResourcePlans",
                value: 1);

            migrationBuilder.UpdateData(
                table: "StageConfigs",
                keyColumn: "Id",
                keyValue: 4,
                column: "MinQualificationTesting",
                value: 1);
        }
    }
}
