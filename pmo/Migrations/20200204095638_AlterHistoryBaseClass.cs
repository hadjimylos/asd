using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class AlterHistoryBaseClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "Risks");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "Risks");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "RampResourcePlans");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "RampResourcePlans");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "QualificationTestings");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "QualificationTestings");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "ProjectJustifications");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "ProjectJustifications");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "ProductIntroChecklists");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "ProductIntroChecklists");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "ProductInfrigmentPatentabilities");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "ProductInfrigmentPatentabilities");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "Mitigations");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "Mitigations");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "KeyCharacteristics");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "KeyCharacteristics");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "InvestmentPlans");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "InvestmentPlans");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "CustomerDesignApprovals");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "CustomerDesignApprovals");

            migrationBuilder.DropColumn(
                name: "EndUpdateDate",
                table: "BusinessCases");

            migrationBuilder.DropColumn(
                name: "StartUpdateDate",
                table: "BusinessCases");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Stages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Stages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Risks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Risks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "RampResourcePlans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "RampResourcePlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "QualificationTestings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "QualificationTestings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProjectJustifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ProjectJustifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProductIntroChecklists",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ProductIntroChecklists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProductInfrigmentPatentabilities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ProductInfrigmentPatentabilities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Mitigations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Mitigations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "KeyCharacteristics",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "KeyCharacteristics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "InvestmentPlans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "InvestmentPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "CustomerDesignApprovals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "CustomerDesignApprovals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "BusinessCases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "BusinessCases",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Risks");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Risks");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "RampResourcePlans");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "RampResourcePlans");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "QualificationTestings");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "QualificationTestings");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProjectJustifications");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ProjectJustifications");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProductIntroChecklists");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ProductIntroChecklists");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProductInfrigmentPatentabilities");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ProductInfrigmentPatentabilities");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Mitigations");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Mitigations");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "KeyCharacteristics");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "KeyCharacteristics");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "InvestmentPlans");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "InvestmentPlans");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "CustomerDesignApprovals");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "CustomerDesignApprovals");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "BusinessCases");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "BusinessCases");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "Stages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "Stages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "Risks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "Risks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "RampResourcePlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "RampResourcePlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "QualificationTestings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "QualificationTestings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "ProjectJustifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "ProjectJustifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "ProjectDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "ProjectDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "ProductIntroChecklists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "ProductIntroChecklists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "ProductInfrigmentPatentabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "ProductInfrigmentPatentabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "Mitigations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "Mitigations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "KeyCharacteristics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "KeyCharacteristics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "InvestmentPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "InvestmentPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "CustomerDesignApprovals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "CustomerDesignApprovals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndUpdateDate",
                table: "BusinessCases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartUpdateDate",
                table: "BusinessCases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
