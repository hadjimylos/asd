using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddDataForOldDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "Name", "TagCategoryId" },
                values: new object[] { 767, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer Specific", 5 });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "Name", "TagCategoryId" },
                values: new object[] { 768, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Santa Ana", 13 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 767);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 768);
        }
    }
}
