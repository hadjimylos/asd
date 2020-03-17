using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class foreignKeyUserId_OnGateKeepers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GateKeeperName",
                table: "GateKeepers");

            migrationBuilder.DropColumn(
                name: "GateKeeperName",
                table: "GateKeeperLites");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GateKeepers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GateKeeperLites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_UserId",
                table: "GateKeepers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperLites_UserId",
                table: "GateKeeperLites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateKeeperLites_Users_UserId",
                table: "GateKeeperLites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GateKeepers_Users_UserId",
                table: "GateKeepers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateKeeperLites_Users_UserId",
                table: "GateKeeperLites");

            migrationBuilder.DropForeignKey(
                name: "FK_GateKeepers_Users_UserId",
                table: "GateKeepers");

            migrationBuilder.DropIndex(
                name: "IX_GateKeepers_UserId",
                table: "GateKeepers");

            migrationBuilder.DropIndex(
                name: "IX_GateKeeperLites_UserId",
                table: "GateKeeperLites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GateKeepers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GateKeeperLites");

            migrationBuilder.AddColumn<string>(
                name: "GateKeeperName",
                table: "GateKeepers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GateKeeperName",
                table: "GateKeeperLites",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
