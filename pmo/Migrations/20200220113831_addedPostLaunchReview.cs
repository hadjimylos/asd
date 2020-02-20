using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class addedPostLaunchReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinPostLaunchReviews",
                table: "StageConfigs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PostLaunchReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    DoneWell = table.Column<string>(nullable: true),
                    DonePoorly = table.Column<string>(nullable: true),
                    Bottlenecks = table.Column<string>(nullable: true),
                    LessonsLearned = table.Column<string>(nullable: true),
                    ActualVSExpected = table.Column<string>(nullable: true),
                    Commercial = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLaunchReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostLaunchReviews_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "StageConfigs",
                keyColumn: "Id",
                keyValue: 5,
                column: "MinPostLaunchReviews",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_PostLaunchReviews_StageId",
                table: "PostLaunchReviews",
                column: "StageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostLaunchReviews");

            migrationBuilder.DropColumn(
                name: "MinPostLaunchReviews",
                table: "StageConfigs");
        }
    }
}
