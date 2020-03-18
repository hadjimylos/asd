using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class NullableUserIdOnItnroChecklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ApprovedByUserId",
                table: "ProductIntroChecklists",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GateId",
                table: "GateFiles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ApprovedByUserId",
                table: "ProductIntroChecklists",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GateId",
                table: "GateFiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
