using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddGateKeeperConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_GateConfigs_GateConfigId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_GateConfigId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "GateConfigId",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "GateKeeperConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Keeper = table.Column<string>(nullable: false),
                    GateConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateKeeperConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateKeeperConfigs_GateConfigs_GateConfigId",
                        column: x => x.GateConfigId,
                        principalTable: "GateConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperConfigs_GateConfigId",
                table: "GateKeeperConfigs",
                column: "GateConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GateKeeperConfigs");

            migrationBuilder.AddColumn<int>(
                name: "GateConfigId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_GateConfigId",
                table: "Roles",
                column: "GateConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_GateConfigs_GateConfigId",
                table: "Roles",
                column: "GateConfigId",
                principalTable: "GateConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
