using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class FixDeliverableRegisterConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliverableRegisterConfigs_Stages_StageId",
                table: "DeliverableRegisterConfigs");

            migrationBuilder.DropIndex(
                name: "IX_DeliverableRegisterConfigs_StageId",
                table: "DeliverableRegisterConfigs");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "DeliverableRegisterConfigs");

            migrationBuilder.AddColumn<int>(
                name: "StageConfigId",
                table: "DeliverableRegisterConfigs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliverableRegisterConfigs_StageConfigId",
                table: "DeliverableRegisterConfigs",
                column: "StageConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliverableRegisterConfigs_StageConfigs_StageConfigId",
                table: "DeliverableRegisterConfigs",
                column: "StageConfigId",
                principalTable: "StageConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliverableRegisterConfigs_StageConfigs_StageConfigId",
                table: "DeliverableRegisterConfigs");

            migrationBuilder.DropIndex(
                name: "IX_DeliverableRegisterConfigs_StageConfigId",
                table: "DeliverableRegisterConfigs");

            migrationBuilder.DropColumn(
                name: "StageConfigId",
                table: "DeliverableRegisterConfigs");

            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "DeliverableRegisterConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliverableRegisterConfigs_StageId",
                table: "DeliverableRegisterConfigs",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliverableRegisterConfigs_Stages_StageId",
                table: "DeliverableRegisterConfigs",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
