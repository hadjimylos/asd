using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class OptionalFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionalFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    FileTagId = table.Column<int>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionalFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionalFiles_Tags_FileTagId",
                        column: x => x.FileTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OptionalFiles_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionalFiles_FileTagId",
                table: "OptionalFiles",
                column: "FileTagId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionalFiles_StageId",
                table: "OptionalFiles",
                column: "StageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionalFiles");
        }
    }
}
