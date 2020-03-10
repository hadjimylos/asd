using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddGateKeeperLiteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GateKeeperLites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    GateKeeperName = table.Column<string>(nullable: true),
                    GateId = table.Column<int>(nullable: false),
                    GateKeeperConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateKeeperLites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateKeeperLites_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateKeeperLites_LiteGateKeeperConfigs_GateKeeperConfigId",
                        column: x => x.GateKeeperConfigId,
                        principalTable: "LiteGateKeeperConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperLites_GateId",
                table: "GateKeeperLites",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperLites_GateKeeperConfigId",
                table: "GateKeeperLites",
                column: "GateKeeperConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GateKeeperLites");
        }
    }
}
