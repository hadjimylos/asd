using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class RefactorDBmodelForReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_BusinessCase_Report_Project_ProjectId",
                table: "Report_BusinessCase");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_BusinessCase_ManufacturingLocations_Report_BusinessCase_BusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_FinancialData_Report_BusinessCase_BusinessCaseId",
                table: "Report_FinancialData");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_ProjectCustomers_Report_Project_ProjectId",
                table: "Report_ProjectCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_ProjectEndUserCountries_Report_Project_ProjectId",
                table: "Report_ProjectEndUserCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_ProjectSalesRegion_Report_Project_ProjectId",
                table: "Report_ProjectSalesRegion");

            migrationBuilder.DropIndex(
                name: "IX_Report_ProjectSalesRegion_ProjectId",
                table: "Report_ProjectSalesRegion");

            migrationBuilder.DropIndex(
                name: "IX_Report_ProjectEndUserCountries_ProjectId",
                table: "Report_ProjectEndUserCountries");

            migrationBuilder.DropIndex(
                name: "IX_Report_ProjectCustomers_ProjectId",
                table: "Report_ProjectCustomers");

            migrationBuilder.DropIndex(
                name: "IX_Report_Project_ProjectId",
                table: "Report_Project");

            migrationBuilder.DropIndex(
                name: "IX_Report_FinancialData_BusinessCaseId",
                table: "Report_FinancialData");

            migrationBuilder.DropIndex(
                name: "IX_Report_BusinessCase_ManufacturingLocations_BusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations");

            migrationBuilder.DropIndex(
                name: "IX_Report_BusinessCase_ProjectId",
                table: "Report_BusinessCase");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Report_ProjectSalesRegion");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Report_ProjectEndUserCountries");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Report_ProjectCustomers");

            migrationBuilder.DropColumn(
                name: "CurrentStageNumber",
                table: "Report_Project");

            migrationBuilder.DropColumn(
                name: "BusinessCaseId",
                table: "Report_FinancialData");

            migrationBuilder.DropColumn(
                name: "BusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Report_BusinessCase");

            migrationBuilder.AddColumn<int>(
                name: "ReportProjectId",
                table: "Report_ProjectSalesRegion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportProjectId",
                table: "Report_ProjectEndUserCountries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportProjectId",
                table: "Report_ProjectCustomers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportBusinessCaseId",
                table: "Report_FinancialData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportBusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportProjectId",
                table: "Report_BusinessCase",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StageNumber",
                table: "Report_BusinessCase",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectSalesRegion_ReportProjectId",
                table: "Report_ProjectSalesRegion",
                column: "ReportProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectEndUserCountries_ReportProjectId",
                table: "Report_ProjectEndUserCountries",
                column: "ReportProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectCustomers_ReportProjectId",
                table: "Report_ProjectCustomers",
                column: "ReportProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Project_ProjectId",
                table: "Report_Project",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_FinancialData_ReportBusinessCaseId",
                table: "Report_FinancialData",
                column: "ReportBusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ManufacturingLocations_ReportBusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations",
                column: "ReportBusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ReportProjectId",
                table: "Report_BusinessCase",
                column: "ReportProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_BusinessCase_Report_Project_ReportProjectId",
                table: "Report_BusinessCase",
                column: "ReportProjectId",
                principalTable: "Report_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_BusinessCase_ManufacturingLocations_Report_BusinessCase_ReportBusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations",
                column: "ReportBusinessCaseId",
                principalTable: "Report_BusinessCase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_FinancialData_Report_BusinessCase_ReportBusinessCaseId",
                table: "Report_FinancialData",
                column: "ReportBusinessCaseId",
                principalTable: "Report_BusinessCase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_ProjectCustomers_Report_Project_ReportProjectId",
                table: "Report_ProjectCustomers",
                column: "ReportProjectId",
                principalTable: "Report_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_ProjectEndUserCountries_Report_Project_ReportProjectId",
                table: "Report_ProjectEndUserCountries",
                column: "ReportProjectId",
                principalTable: "Report_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_ProjectSalesRegion_Report_Project_ReportProjectId",
                table: "Report_ProjectSalesRegion",
                column: "ReportProjectId",
                principalTable: "Report_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_BusinessCase_Report_Project_ReportProjectId",
                table: "Report_BusinessCase");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_BusinessCase_ManufacturingLocations_Report_BusinessCase_ReportBusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_FinancialData_Report_BusinessCase_ReportBusinessCaseId",
                table: "Report_FinancialData");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_ProjectCustomers_Report_Project_ReportProjectId",
                table: "Report_ProjectCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_ProjectEndUserCountries_Report_Project_ReportProjectId",
                table: "Report_ProjectEndUserCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_ProjectSalesRegion_Report_Project_ReportProjectId",
                table: "Report_ProjectSalesRegion");

            migrationBuilder.DropIndex(
                name: "IX_Report_ProjectSalesRegion_ReportProjectId",
                table: "Report_ProjectSalesRegion");

            migrationBuilder.DropIndex(
                name: "IX_Report_ProjectEndUserCountries_ReportProjectId",
                table: "Report_ProjectEndUserCountries");

            migrationBuilder.DropIndex(
                name: "IX_Report_ProjectCustomers_ReportProjectId",
                table: "Report_ProjectCustomers");

            migrationBuilder.DropIndex(
                name: "IX_Report_Project_ProjectId",
                table: "Report_Project");

            migrationBuilder.DropIndex(
                name: "IX_Report_FinancialData_ReportBusinessCaseId",
                table: "Report_FinancialData");

            migrationBuilder.DropIndex(
                name: "IX_Report_BusinessCase_ManufacturingLocations_ReportBusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations");

            migrationBuilder.DropIndex(
                name: "IX_Report_BusinessCase_ReportProjectId",
                table: "Report_BusinessCase");

            migrationBuilder.DropColumn(
                name: "ReportProjectId",
                table: "Report_ProjectSalesRegion");

            migrationBuilder.DropColumn(
                name: "ReportProjectId",
                table: "Report_ProjectEndUserCountries");

            migrationBuilder.DropColumn(
                name: "ReportProjectId",
                table: "Report_ProjectCustomers");

            migrationBuilder.DropColumn(
                name: "ReportBusinessCaseId",
                table: "Report_FinancialData");

            migrationBuilder.DropColumn(
                name: "ReportBusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations");

            migrationBuilder.DropColumn(
                name: "ReportProjectId",
                table: "Report_BusinessCase");

            migrationBuilder.DropColumn(
                name: "StageNumber",
                table: "Report_BusinessCase");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Report_ProjectSalesRegion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Report_ProjectEndUserCountries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Report_ProjectCustomers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentStageNumber",
                table: "Report_Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusinessCaseId",
                table: "Report_FinancialData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Report_BusinessCase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectSalesRegion_ProjectId",
                table: "Report_ProjectSalesRegion",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectEndUserCountries_ProjectId",
                table: "Report_ProjectEndUserCountries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectCustomers_ProjectId",
                table: "Report_ProjectCustomers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Project_ProjectId",
                table: "Report_Project",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_FinancialData_BusinessCaseId",
                table: "Report_FinancialData",
                column: "BusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ManufacturingLocations_BusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations",
                column: "BusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ProjectId",
                table: "Report_BusinessCase",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_BusinessCase_Report_Project_ProjectId",
                table: "Report_BusinessCase",
                column: "ProjectId",
                principalTable: "Report_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_BusinessCase_ManufacturingLocations_Report_BusinessCase_BusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations",
                column: "BusinessCaseId",
                principalTable: "Report_BusinessCase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_FinancialData_Report_BusinessCase_BusinessCaseId",
                table: "Report_FinancialData",
                column: "BusinessCaseId",
                principalTable: "Report_BusinessCase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_ProjectCustomers_Report_Project_ProjectId",
                table: "Report_ProjectCustomers",
                column: "ProjectId",
                principalTable: "Report_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_ProjectEndUserCountries_Report_Project_ProjectId",
                table: "Report_ProjectEndUserCountries",
                column: "ProjectId",
                principalTable: "Report_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_ProjectSalesRegion_Report_Project_ProjectId",
                table: "Report_ProjectSalesRegion",
                column: "ProjectId",
                principalTable: "Report_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
