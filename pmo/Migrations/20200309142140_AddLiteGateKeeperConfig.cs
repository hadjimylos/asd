using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddLiteGateKeeperConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LiteGateKeeperConfigId",
                table: "GateKeepers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LiteGateKeeperConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Keeper = table.Column<string>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteGateKeeperConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiteGateKeeperConfigs_LiteStageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "LiteStageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_LiteGateKeeperConfigId",
                table: "GateKeepers",
                column: "LiteGateKeeperConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteGateKeeperConfigs_StageConfigId",
                table: "LiteGateKeeperConfigs",
                column: "StageConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateKeepers_LiteGateKeeperConfigs_LiteGateKeeperConfigId",
                table: "GateKeepers",
                column: "LiteGateKeeperConfigId",
                principalTable: "LiteGateKeeperConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateKeepers_LiteGateKeeperConfigs_LiteGateKeeperConfigId",
                table: "GateKeepers");

            migrationBuilder.DropTable(
                name: "LiteGateKeeperConfigs");

            migrationBuilder.DropIndex(
                name: "IX_GateKeepers_LiteGateKeeperConfigId",
                table: "GateKeepers");

            migrationBuilder.DropColumn(
                name: "LiteGateKeeperConfigId",
                table: "GateKeepers");
        }
    }
}
