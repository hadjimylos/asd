using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class FixAdditionalForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StageFiles_Tags_RequiredScheduleTagId",
                table: "StageFiles");

            migrationBuilder.DropIndex(
                name: "IX_StageFiles_RequiredScheduleTagId",
                table: "StageFiles");

            migrationBuilder.DropColumn(
                name: "RequiredScheduleTagId",
                table: "StageFiles");

            migrationBuilder.CreateIndex(
                name: "IX_StageFiles_FileTagId",
                table: "StageFiles",
                column: "FileTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_StageFiles_Tags_FileTagId",
                table: "StageFiles",
                column: "FileTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StageFiles_Tags_FileTagId",
                table: "StageFiles");

            migrationBuilder.DropIndex(
                name: "IX_StageFiles_FileTagId",
                table: "StageFiles");

            migrationBuilder.AddColumn<int>(
                name: "RequiredScheduleTagId",
                table: "StageFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StageFiles_RequiredScheduleTagId",
                table: "StageFiles",
                column: "RequiredScheduleTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_StageFiles_Tags_RequiredScheduleTagId",
                table: "StageFiles",
                column: "RequiredScheduleTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
