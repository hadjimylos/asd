using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddVBPDLite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LiteStageConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StageNumber = table.Column<int>(nullable: false),
                    AllowInsertRiskAssesments = table.Column<bool>(nullable: false),
                    MinProjectJustifications = table.Column<int>(nullable: false),
                    MinBusinessCases = table.Column<int>(nullable: false),
                    MinProductInfringementPatentabilities = table.Column<int>(nullable: false),
                    MinKeyCharacteristics = table.Column<int>(nullable: false),
                    MinCustomerDesignApprovals = table.Column<int>(nullable: false),
                    MinInvestmentPlans = table.Column<int>(nullable: false),
                    MinProductIntroChecklist = table.Column<int>(nullable: false),
                    MinPostLaunchReviews = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteStageConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LiteRequiredSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RequiredScheduleTagId = table.Column<int>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteRequiredSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiteRequiredSchedules_Tags_RequiredScheduleTagId",
                        column: x => x.RequiredScheduleTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiteRequiredSchedules_LiteStageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "LiteStageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiteStageFileConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RequiredFileTagId = table.Column<int>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteStageFileConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiteStageFileConfigs_Tags_RequiredFileTagId",
                        column: x => x.RequiredFileTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiteStageFileConfigs_LiteStageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "LiteStageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiteRequiredSchedules_RequiredScheduleTagId",
                table: "LiteRequiredSchedules",
                column: "RequiredScheduleTagId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteRequiredSchedules_StageConfigId",
                table: "LiteRequiredSchedules",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteStageFileConfigs_RequiredFileTagId",
                table: "LiteStageFileConfigs",
                column: "RequiredFileTagId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteStageFileConfigs_StageConfigId",
                table: "LiteStageFileConfigs",
                column: "StageConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiteRequiredSchedules");

            migrationBuilder.DropTable(
                name: "LiteStageFileConfigs");

            migrationBuilder.DropTable(
                name: "LiteStageConfigs");
        }
    }
}
