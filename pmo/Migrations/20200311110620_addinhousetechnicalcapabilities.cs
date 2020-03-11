using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class addinhousetechnicalcapabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddToInhouseTechnicalAbilities",
                table: "ProjectJustifications");

            migrationBuilder.AddColumn<int>(
                name: "AddToInhouseTechnicalAbilitiesTagId",
                table: "ProjectJustifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "TagCategories",
                columns: new[] { "Id", "CreateDate", "FriendlyName", "IsFixed", "Key", "ModifiedByUser" },
                values: new object[] { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technical Capabilities", false, "technical-capabilities", null });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "Name", "TagCategoryId" },
                values: new object[] { 769, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "None", 16 });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "Name", "TagCategoryId" },
                values: new object[] { 770, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Some", 16 });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "Name", "TagCategoryId" },
                values: new object[] { 771, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Significant", 16 });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJustifications_AddToInhouseTechnicalAbilitiesTagId",
                table: "ProjectJustifications",
                column: "AddToInhouseTechnicalAbilitiesTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectJustifications_Tags_AddToInhouseTechnicalAbilitiesTagId",
                table: "ProjectJustifications",
                column: "AddToInhouseTechnicalAbilitiesTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectJustifications_Tags_AddToInhouseTechnicalAbilitiesTagId",
                table: "ProjectJustifications");

            migrationBuilder.DropIndex(
                name: "IX_ProjectJustifications_AddToInhouseTechnicalAbilitiesTagId",
                table: "ProjectJustifications");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 769);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 770);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 771);

            migrationBuilder.DeleteData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DropColumn(
                name: "AddToInhouseTechnicalAbilitiesTagId",
                table: "ProjectJustifications");

            migrationBuilder.AddColumn<bool>(
                name: "AddToInhouseTechnicalAbilities",
                table: "ProjectJustifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
