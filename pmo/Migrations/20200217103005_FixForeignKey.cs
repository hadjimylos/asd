using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class FixForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StageFileConfigs_Tags_RequiredScheduleTagId",
                table: "StageFileConfigs");

            migrationBuilder.DropIndex(
                name: "IX_StageFileConfigs_RequiredScheduleTagId",
                table: "StageFileConfigs");

            migrationBuilder.DropColumn(
                name: "RequiredScheduleTagId",
                table: "StageFileConfigs");

            migrationBuilder.CreateIndex(
                name: "IX_StageFileConfigs_RequiredFileTagId",
                table: "StageFileConfigs",
                column: "RequiredFileTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_StageFileConfigs_Tags_RequiredFileTagId",
                table: "StageFileConfigs",
                column: "RequiredFileTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StageFileConfigs_Tags_RequiredFileTagId",
                table: "StageFileConfigs");

            migrationBuilder.DropIndex(
                name: "IX_StageFileConfigs_RequiredFileTagId",
                table: "StageFileConfigs");

            migrationBuilder.AddColumn<int>(
                name: "RequiredScheduleTagId",
                table: "StageFileConfigs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StageFileConfigs_RequiredScheduleTagId",
                table: "StageFileConfigs",
                column: "RequiredScheduleTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_StageFileConfigs_Tags_RequiredScheduleTagId",
                table: "StageFileConfigs",
                column: "RequiredScheduleTagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
