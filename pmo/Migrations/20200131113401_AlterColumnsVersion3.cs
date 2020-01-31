using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AlterColumnsVersion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.UpdateData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "FriendlyName", "Key" },
                values: new object[] { "Export Application Type", "export-application-type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "FriendlyName", "Key" },
                values: new object[] { "Design Authority", "design-authority" });

            migrationBuilder.InsertData(
                table: "TagCategories",
                columns: new[] { "Id", "CreateDate", "FriendlyName", "IsFixed", "Key", "ModifiedByUser" },
                values: new object[] { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design Authority", true, "design-authority", null });

            migrationBuilder.InsertData(
                table: "TagCategories",
                columns: new[] { "Id", "CreateDate", "FriendlyName", "IsFixed", "Key", "ModifiedByUser" },
                values: new object[] { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design Authority", true, "design-authority", null });
        }
    }
}
