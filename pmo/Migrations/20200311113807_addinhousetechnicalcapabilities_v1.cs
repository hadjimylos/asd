using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class addinhousetechnicalcapabilities_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectJustifications_Tags_ProductTagId",
                table: "ProjectJustifications");

            migrationBuilder.DropIndex(
                name: "IX_ProjectJustifications_ProductTagId",
                table: "ProjectJustifications");

            migrationBuilder.DropColumn(
                name: "ProductTagId",
                table: "ProjectJustifications");

            migrationBuilder.AddColumn<bool>(
                name: "Product",
                table: "ProjectJustifications",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product",
                table: "ProjectJustifications");

            migrationBuilder.AddColumn<int>(
                name: "ProductTagId",
                table: "ProjectJustifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJustifications_ProductTagId",
                table: "ProjectJustifications",
                column: "ProductTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectJustifications_Tags_ProductTagId",
                table: "ProjectJustifications",
                column: "ProductTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
