using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class RemoveDesignConcept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesignConcepts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DesignConcepts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    Upload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessCaseId = table.Column<int>(type: "int", nullable: true),
                    StageId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignConcepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignConcepts_BusinessCases_BusinessCaseId",
                        column: x => x.BusinessCaseId,
                        principalTable: "BusinessCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignConcepts_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignConcepts_Stages_StageId1",
                        column: x => x.StageId1,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_BusinessCaseId",
                table: "DesignConcepts",
                column: "BusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_StageId",
                table: "DesignConcepts",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_StageId1",
                table: "DesignConcepts",
                column: "StageId1");
        }
    }
}
