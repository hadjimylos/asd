using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class ProductIntroChecklist_AddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "ProductIntroChecklists");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByUserId",
                table: "ProductIntroChecklists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductIntroChecklists_ApprovedByUserId",
                table: "ProductIntroChecklists",
                column: "ApprovedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIntroChecklists_Users_ApprovedByUserId",
                table: "ProductIntroChecklists",
                column: "ApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIntroChecklists_Users_ApprovedByUserId",
                table: "ProductIntroChecklists");

            migrationBuilder.DropIndex(
                name: "IX_ProductIntroChecklists_ApprovedByUserId",
                table: "ProductIntroChecklists");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserId",
                table: "ProductIntroChecklists");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "ProductIntroChecklists",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
