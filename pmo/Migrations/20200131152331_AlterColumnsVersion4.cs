using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AlterColumnsVersion4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetails_Tags_CustomerTagId",
                table: "ProjectDetails");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDetails_CustomerTagId",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "CustomerTagId",
                table: "ProjectDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerTagId",
                table: "ProjectDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_CustomerTagId",
                table: "ProjectDetails",
                column: "CustomerTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetails_Tags_CustomerTagId",
                table: "ProjectDetails",
                column: "CustomerTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
