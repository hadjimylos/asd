using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddStageFileTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StageFileConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RequiredFileTagId = table.Column<int>(nullable: false),
                    RequiredScheduleTagId = table.Column<int>(nullable: true),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageFileConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageFileConfigs_Tags_RequiredScheduleTagId",
                        column: x => x.RequiredScheduleTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageFileConfigs_StageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "StageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StageFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    FileTagId = table.Column<int>(nullable: false),
                    RequiredScheduleTagId = table.Column<int>(nullable: true),
                    StageId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageFiles_Tags_RequiredScheduleTagId",
                        column: x => x.RequiredScheduleTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageFiles_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StageFileConfigs_RequiredScheduleTagId",
                table: "StageFileConfigs",
                column: "RequiredScheduleTagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageFileConfigs_StageConfigId",
                table: "StageFileConfigs",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_StageFiles_RequiredScheduleTagId",
                table: "StageFiles",
                column: "RequiredScheduleTagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageFiles_StageId",
                table: "StageFiles",
                column: "StageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StageFileConfigs");

            migrationBuilder.DropTable(
                name: "StageFiles");
        }
    }
}
