using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class RemoveDilverableRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliverableRegisterConfigs");

            migrationBuilder.DropTable(
                name: "DeliverableRegisters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliverableRegisterConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StageConfigId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverableRegisterConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliverableRegisterConfigs_StageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "StageConfigs",
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_DeliverableRegisterConfigs_StageConfigId",
                table: "DeliverableRegisterConfigs",
                column: "StageConfigId");

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
    }
}
