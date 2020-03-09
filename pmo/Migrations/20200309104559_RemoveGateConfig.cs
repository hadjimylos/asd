using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class RemoveGateConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateKeeperConfigs_GateConfigs_GateConfigId",
                table: "GateKeeperConfigs");

            migrationBuilder.DropForeignKey(
                name: "FK_Gates_GateConfigs_GateConfigId",
                table: "Gates");

            migrationBuilder.DropTable(
                name: "GateConfigs");

            migrationBuilder.DropIndex(
                name: "IX_Gates_GateConfigId",
                table: "Gates");

            migrationBuilder.DropIndex(
                name: "IX_GateKeeperConfigs_GateConfigId",
                table: "GateKeeperConfigs");

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "StageConfig_RequiredSchedules",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DropColumn(
                name: "GateConfigId",
                table: "Gates");

            migrationBuilder.DropColumn(
                name: "GateConfigId",
                table: "GateKeeperConfigs");

            migrationBuilder.AddColumn<int>(
                name: "StageConfigId",
                table: "Gates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StageConfigId",
                table: "GateKeeperConfigs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "StageConfigId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 2,
                column: "StageConfigId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 3,
                column: "StageConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 4,
                column: "StageConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 5,
                column: "StageConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 6,
                column: "StageConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 7,
                column: "StageConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 8,
                column: "StageConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 9,
                column: "StageConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 10,
                column: "StageConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 11,
                column: "StageConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 12,
                column: "StageConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 13,
                column: "StageConfigId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 14,
                column: "StageConfigId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 15,
                column: "StageConfigId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 16,
                column: "StageConfigId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 17,
                column: "StageConfigId",
                value: 2);

            migrationBuilder.InsertData(
                table: "LiteStageConfigs",
                columns: new[] { "Id", "AllowInsertRiskAssesments", "CreateDate", "MinBusinessCases", "MinCustomerDesignApprovals", "MinInvestmentPlans", "MinKeyCharacteristics", "MinPostLaunchReviews", "MinProductInfringementPatentabilities", "MinProductIntroChecklist", "MinProjectJustifications", "ModifiedByUser", "StageNumber" },
                values: new object[,]
                {
                    { 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, 1, 0, 0, 0, 1, 0, null, 0 },
                    { 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, 0, 0, 0, 0, 0, 1, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "LiteStageFileConfigs",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "RequiredFileTagId", "StageConfigId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 761, 2 });

            migrationBuilder.InsertData(
                table: "LiteStageFileConfigs",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "RequiredFileTagId", "StageConfigId" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 762, 2 });

            migrationBuilder.InsertData(
                table: "LiteStageFileConfigs",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "RequiredFileTagId", "StageConfigId" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 765, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Gates_StageConfigId",
                table: "Gates",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperConfigs_StageConfigId",
                table: "GateKeeperConfigs",
                column: "StageConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateKeeperConfigs_StageConfigs_StageConfigId",
                table: "GateKeeperConfigs",
                column: "StageConfigId",
                principalTable: "StageConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gates_StageConfigs_StageConfigId",
                table: "Gates",
                column: "StageConfigId",
                principalTable: "StageConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateKeeperConfigs_StageConfigs_StageConfigId",
                table: "GateKeeperConfigs");

            migrationBuilder.DropForeignKey(
                name: "FK_Gates_StageConfigs_StageConfigId",
                table: "Gates");

            migrationBuilder.DropIndex(
                name: "IX_Gates_StageConfigId",
                table: "Gates");

            migrationBuilder.DropIndex(
                name: "IX_GateKeeperConfigs_StageConfigId",
                table: "GateKeeperConfigs");

            migrationBuilder.DeleteData(
                table: "LiteStageConfigs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LiteStageFileConfigs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LiteStageFileConfigs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LiteStageFileConfigs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LiteStageConfigs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "StageConfigId",
                table: "Gates");

            migrationBuilder.DropColumn(
                name: "StageConfigId",
                table: "GateKeeperConfigs");

            migrationBuilder.AddColumn<int>(
                name: "GateConfigId",
                table: "Gates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GateConfigId",
                table: "GateKeeperConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GateConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GateNumber = table.Column<int>(type: "int", nullable: false),
                    ModifiedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateConfigs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GateConfigs",
                columns: new[] { "Id", "CreateDate", "GateNumber", "ModifiedByUser" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "system" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "system" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "system" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "system" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "system" }
                });

            migrationBuilder.InsertData(
                table: "StageConfig_RequiredSchedules",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "RequiredScheduleTagId", "StageConfigId" },
                values: new object[,]
                {
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 13, 3 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 12, 3 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 11, 3 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 10, 3 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 9, 3 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 8, 3 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 5, 3 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 6, 3 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 14, 3 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 4, 3 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 3, 3 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 2, 3 },
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 1, 3 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 7, 3 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 15, 3 }
                });

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 1,
                column: "GateConfigId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 2,
                column: "GateConfigId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 3,
                column: "GateConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 4,
                column: "GateConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 5,
                column: "GateConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 6,
                column: "GateConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 7,
                column: "GateConfigId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 8,
                column: "GateConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 9,
                column: "GateConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 10,
                column: "GateConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 11,
                column: "GateConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 12,
                column: "GateConfigId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 13,
                column: "GateConfigId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 14,
                column: "GateConfigId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 15,
                column: "GateConfigId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 16,
                column: "GateConfigId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "GateKeeperConfigs",
                keyColumn: "Id",
                keyValue: 17,
                column: "GateConfigId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Gates_GateConfigId",
                table: "Gates",
                column: "GateConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperConfigs_GateConfigId",
                table: "GateKeeperConfigs",
                column: "GateConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateKeeperConfigs_GateConfigs_GateConfigId",
                table: "GateKeeperConfigs",
                column: "GateConfigId",
                principalTable: "GateConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gates_GateConfigs_GateConfigId",
                table: "Gates",
                column: "GateConfigId",
                principalTable: "GateConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
