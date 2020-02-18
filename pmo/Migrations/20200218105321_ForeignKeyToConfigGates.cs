using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class ForeignKeyToConfigGates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GateConfigId",
                table: "Gates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gates_GateConfigId",
                table: "Gates",
                column: "GateConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gates_GateConfigs_GateConfigId",
                table: "Gates",
                column: "GateConfigId",
                principalTable: "GateConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gates_GateConfigs_GateConfigId",
                table: "Gates");

            migrationBuilder.DropIndex(
                name: "IX_Gates_GateConfigId",
                table: "Gates");

            migrationBuilder.DropColumn(
                name: "GateConfigId",
                table: "Gates");
        }
    }
}
