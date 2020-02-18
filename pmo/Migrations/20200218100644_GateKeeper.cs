using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class GateKeeper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GateApprovers");

            migrationBuilder.CreateTable(
                name: "GateKeepers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    GateKeeperConfigId = table.Column<int>(nullable: false),
                    GateKeeperName = table.Column<string>(nullable: true),
                    GateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateKeepers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateKeepers_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateKeepers_GateKeeperConfigs_GateKeeperConfigId",
                        column: x => x.GateKeeperConfigId,
                        principalTable: "GateKeeperConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_GateId",
                table: "GateKeepers",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_GateKeeperConfigId",
                table: "GateKeepers",
                column: "GateKeeperConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GateKeepers");

            migrationBuilder.CreateTable(
                name: "GateApprovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GateApproverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GateId = table.Column<int>(type: "int", nullable: false),
                    ModifiedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateApprovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateApprovers_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GateApprovers_GateId",
                table: "GateApprovers",
                column: "GateId");
        }
    }
}
