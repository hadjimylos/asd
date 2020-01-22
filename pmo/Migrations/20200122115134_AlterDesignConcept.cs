using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AlterDesignConcept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignConcepts_Tags_DeliverableRegisterId",
                table: "DesignConcepts");

            migrationBuilder.DropTable(
                name: "StageConfig_RequiredDesignConcepts");

            migrationBuilder.DropIndex(
                name: "IX_DesignConcepts_DeliverableRegisterId",
                table: "DesignConcepts");

            migrationBuilder.DropColumn(
                name: "DeliverableRegisterId",
                table: "DesignConcepts");

            migrationBuilder.DropColumn(
                name: "DesignConceptType",
                table: "DesignConcepts");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "DesignConcepts");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "DesignConcepts");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "DesignConcepts");

            migrationBuilder.AlterColumn<int>(
                name: "MinRampResourcePlans",
                table: "StageConfigs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinQualificationTesting",
                table: "StageConfigs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinProjectJustifications",
                table: "StageConfigs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinProductIntroChecklist",
                table: "StageConfigs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinProductInfringementPatentabilities",
                table: "StageConfigs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinKeyCharacteristics",
                table: "StageConfigs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinInvestmentPlans",
                table: "StageConfigs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinCustomerDesignApprovals",
                table: "StageConfigs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinBusinessCases",
                table: "StageConfigs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinDesignConcepts",
                table: "StageConfigs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "DesignConcepts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Upload",
                table: "DesignConcepts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StageId1",
                table: "DesignConcepts",
                nullable: true);

            migrationBuilder.InsertData(
                table: "TagCategories",
                columns: new[] { "Id", "CreateDate", "FriendlyName", "IsFixed", "Key", "ModifiedByUser" },
                values: new object[] { 1, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), "Required Schedules", true, "required-schedules", null });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "Name", "TagCategoryId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Gate 1", 1 },
                    { 2, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Design concept(s)", 1 },
                    { 3, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Gate 2 / Gate A", 1 },
                    { 4, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Draft manufacturing drawings", 1 },
                    { 5, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Design Review", 1 },
                    { 6, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Submit PAR", 1 },
                    { 7, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Release Product Documentation", 1 },
                    { 8, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Create Samples / Prototypes", 1 },
                    { 9, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "DVT testing & Test Report", 1 },
                    { 10, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Gate 3", 1 },
                    { 11, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "First Article Approval", 1 },
                    { 12, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Qualification Testing & Test Report", 1 },
                    { 13, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Customer Approval / Release", 1 },
                    { 14, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "Gate 4 / Gate B", 1 },
                    { 15, new DateTime(2020, 1, 22, 13, 51, 33, 452, DateTimeKind.Local).AddTicks(2005), null, "PLR Review", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_StageId1",
                table: "DesignConcepts",
                column: "StageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignConcepts_Stages_StageId1",
                table: "DesignConcepts",
                column: "StageId1",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignConcepts_Stages_StageId1",
                table: "DesignConcepts");

            migrationBuilder.DropIndex(
                name: "IX_DesignConcepts_StageId1",
                table: "DesignConcepts");

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TagCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "MinDesignConcepts",
                table: "StageConfigs");

            migrationBuilder.DropColumn(
                name: "Upload",
                table: "DesignConcepts");

            migrationBuilder.DropColumn(
                name: "StageId1",
                table: "DesignConcepts");

            migrationBuilder.AlterColumn<int>(
                name: "MinRampResourcePlans",
                table: "StageConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MinQualificationTesting",
                table: "StageConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MinProjectJustifications",
                table: "StageConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MinProductIntroChecklist",
                table: "StageConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MinProductInfringementPatentabilities",
                table: "StageConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MinKeyCharacteristics",
                table: "StageConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MinInvestmentPlans",
                table: "StageConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MinCustomerDesignApprovals",
                table: "StageConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MinBusinessCases",
                table: "StageConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "DesignConcepts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DeliverableRegisterId",
                table: "DesignConcepts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DesignConceptType",
                table: "DesignConcepts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "DesignConcepts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "DesignConcepts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "DesignConcepts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "StageConfig_RequiredDesignConcepts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredDesignConceptTagId = table.Column<int>(type: "int", nullable: false),
                    StageConfigId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageConfig_RequiredDesignConcepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageConfig_RequiredDesignConcepts_Tags_RequiredDesignConceptTagId",
                        column: x => x.RequiredDesignConceptTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageConfig_RequiredDesignConcepts_StageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "StageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_DeliverableRegisterId",
                table: "DesignConcepts",
                column: "DeliverableRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredDesignConcepts_RequiredDesignConceptTagId",
                table: "StageConfig_RequiredDesignConcepts",
                column: "RequiredDesignConceptTagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredDesignConcepts_StageConfigId",
                table: "StageConfig_RequiredDesignConcepts",
                column: "StageConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignConcepts_Tags_DeliverableRegisterId",
                table: "DesignConcepts",
                column: "DeliverableRegisterId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
