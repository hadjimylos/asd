using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations {
    public partial class FixObjectNames : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_StageConfig_RequiredSchedules_Tags_RequiredSchedulesTagId",
                table: "StageConfig_RequiredSchedules");

            migrationBuilder.DropIndex(
                name: "IX_StageConfig_RequiredSchedules_RequiredSchedulesTagId",
                table: "StageConfig_RequiredSchedules");

            migrationBuilder.DropColumn(
                name: "RequiredSchedulesTagId",
                table: "StageConfig_RequiredSchedules");

            migrationBuilder.AddColumn<int>(
                name: "RequiredScheduleTagId",
                table: "StageConfig_RequiredSchedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 70,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 71,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 72,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 73,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 74,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 75,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 76,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 77,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 78,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 79,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 80,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 81,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 82,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 83,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 84,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 85,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 86,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 87,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 88,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 89,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 90,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 91,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 92,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 93,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 94,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 95,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 96,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 97,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 98,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 99,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 128,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 129,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 130,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 131,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 132,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 133,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 134,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 135,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 136,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 137,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 138,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 139,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 140,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 141,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 142,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 143,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 144,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 145,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 146,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 147,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 148,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 149,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 150,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 151,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 152,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 153,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 154,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 155,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 156,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 157,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 158,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 159,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 160,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 161,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 162,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 163,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 164,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 165,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 166,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 167,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 168,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 169,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 170,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 171,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 172,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 173,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 174,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 175,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 176,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 177,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 178,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 179,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 180,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 181,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 182,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 183,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 184,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 185,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 186,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 187,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 188,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 189,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 190,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 191,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 192,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 193,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 194,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 195,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 196,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 197,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 198,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 199,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 200,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 201,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 202,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 203,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 204,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 205,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 206,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 207,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 208,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 209,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 210,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 211,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 212,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 213,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 214,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 15, 17, 14, 380, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredSchedules_RequiredScheduleTagId",
                table: "StageConfig_RequiredSchedules",
                column: "RequiredScheduleTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_StageConfig_RequiredSchedules_Tags_RequiredScheduleTagId",
                table: "StageConfig_RequiredSchedules",
                column: "RequiredScheduleTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_StageConfig_RequiredSchedules_Tags_RequiredScheduleTagId",
                table: "StageConfig_RequiredSchedules");

            migrationBuilder.DropIndex(
                name: "IX_StageConfig_RequiredSchedules_RequiredScheduleTagId",
                table: "StageConfig_RequiredSchedules");

            migrationBuilder.DropColumn(
                name: "RequiredScheduleTagId",
                table: "StageConfig_RequiredSchedules");

            migrationBuilder.AddColumn<int>(
                name: "RequiredSchedulesTagId",
                table: "StageConfig_RequiredSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 70,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 71,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 72,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 73,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 74,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 75,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 76,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 77,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 78,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 79,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 80,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 81,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 82,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 83,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 84,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 85,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 86,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 87,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 88,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 89,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 90,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 91,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 92,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 93,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 94,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 95,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 96,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 97,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 98,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 99,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 128,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 129,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 130,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 131,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 132,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 133,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 134,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 135,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 136,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 137,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 138,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 139,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 140,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 141,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 142,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 143,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 144,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 145,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 146,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 147,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 148,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 149,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 150,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 151,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 152,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 153,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 154,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 155,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 156,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 157,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 158,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 159,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 160,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 161,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 162,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 163,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 164,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 165,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 166,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 167,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 168,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 169,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 170,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 171,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 172,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 173,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 174,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 175,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 176,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 177,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 178,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 179,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 180,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 181,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 182,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 183,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 184,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 185,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 186,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 187,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 188,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 189,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 190,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 191,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 192,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 193,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 194,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 195,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 196,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 197,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 198,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 199,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 200,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 201,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 202,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 203,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 204,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 205,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 206,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 207,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 208,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 209,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 210,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 211,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 212,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 213,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 214,
                column: "CreateDate",
                value: new DateTime(2020, 1, 22, 14, 50, 39, 321, DateTimeKind.Local).AddTicks(6307));

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredSchedules_RequiredSchedulesTagId",
                table: "StageConfig_RequiredSchedules",
                column: "RequiredSchedulesTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_StageConfig_RequiredSchedules_Tags_RequiredSchedulesTagId",
                table: "StageConfig_RequiredSchedules",
                column: "RequiredSchedulesTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
