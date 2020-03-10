using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class updateSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LiteStageConfigs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedByUser", "StageNumber" },
                values: new object[] { "system", 1 });

            migrationBuilder.UpdateData(
                table: "LiteStageConfigs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedByUser", "StageNumber" },
                values: new object[] { "system", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LiteStageConfigs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ModifiedByUser", "StageNumber" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "LiteStageConfigs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ModifiedByUser", "StageNumber" },
                values: new object[] { null, 0 });
        }
    }
}
