using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AddDeliverableRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliverableRegisterConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StageId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverableRegisterConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliverableRegisterConfigs_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliverableRegisterConfigs_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliverableRegisters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StageId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverableRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliverableRegisters_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliverableRegisters_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliverableRegisterConfigs_StageId",
                table: "DeliverableRegisterConfigs",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliverableRegisterConfigs_TagId",
                table: "DeliverableRegisterConfigs",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliverableRegisters_StageId",
                table: "DeliverableRegisters",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliverableRegisters_TagId",
                table: "DeliverableRegisters",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliverableRegisterConfigs");

            migrationBuilder.DropTable(
                name: "DeliverableRegisters");
        }
    }
}
