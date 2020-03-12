using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddComponentTagCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TagCategories",
                columns: new[] { "Id", "CreateDate", "FriendlyName", "IsFixed", "Key", "ModifiedByUser" },
                values: new object[] { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Application Components", true, "application-components", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 17);
        }
    }
}
