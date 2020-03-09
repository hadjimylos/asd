using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class BI_DBObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Report_Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CurrentStageNumber = table.Column<int>(nullable: false),
                    Salesforce = table.Column<string>(nullable: false),
                    ProjectCategoryTagId = table.Column<int>(nullable: false),
                    ProductLineTagId = table.Column<int>(nullable: false),
                    ProjectClassificationTagId = table.Column<int>(nullable: false),
                    ExportApplicationTypeTagId = table.Column<int>(nullable: false),
                    DesignAuthorityTagId = table.Column<int>(nullable: false),
                    ProjectProcessType = table.Column<string>(nullable: false),
                    ExportControlCode = table.Column<string>(nullable: false),
                    EndUseDestinationCountry = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Project_Tags_DesignAuthorityTagId",
                        column: x => x.DesignAuthorityTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_Project_Tags_ExportApplicationTypeTagId",
                        column: x => x.ExportApplicationTypeTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_Project_Tags_ProductLineTagId",
                        column: x => x.ProductLineTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_Project_Tags_ProjectCategoryTagId",
                        column: x => x.ProjectCategoryTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_Project_Tags_ProjectClassificationTagId",
                        column: x => x.ProjectClassificationTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_Project_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report_BusinessCase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    WillCustomerFundQual = table.Column<bool>(nullable: false),
                    WillCustomerFundTooling = table.Column<bool>(nullable: false),
                    ProbabiltyOfWin = table.Column<decimal>(nullable: false),
                    TargetFirstYearGrossMargin = table.Column<decimal>(nullable: false),
                    FinancialStartYear = table.Column<int>(nullable: false),
                    DiscountRate = table.Column<decimal>(nullable: false),
                    TaxRate = table.Column<decimal>(nullable: false),
                    LaborRate = table.Column<decimal>(nullable: false),
                    GpaRequirements = table.Column<string>(nullable: true),
                    ProjectScope = table.Column<string>(nullable: true),
                    WorkRequirementAmount = table.Column<decimal>(nullable: false),
                    StrategicAlignment = table.Column<bool>(nullable: false),
                    AddToTecnicalAbilities = table.Column<string>(nullable: true),
                    ProjectCompletion = table.Column<DateTime>(nullable: false),
                    TimeFromProjectCompletionToRevenueGeneration = table.Column<decimal>(nullable: false),
                    CustomerMarketAnalysis = table.Column<string>(nullable: true),
                    Changes = table.Column<bool>(nullable: false),
                    GetNPV = table.Column<decimal>(nullable: false),
                    GetROI = table.Column<decimal>(nullable: false),
                    GetPaybackPeriod = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_BusinessCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_BusinessCase_Report_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Report_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report_ProjectCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomersTagId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_ProjectCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_ProjectCustomers_Tags_CustomersTagId",
                        column: x => x.CustomersTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_ProjectCustomers_Report_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Report_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report_ProjectEndUserCountries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndUserCountriesTagId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_ProjectEndUserCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_ProjectEndUserCountries_Tags_EndUserCountriesTagId",
                        column: x => x.EndUserCountriesTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_ProjectEndUserCountries_Report_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Report_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report_ProjectSalesRegion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesRegionTagId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_ProjectSalesRegion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_ProjectSalesRegion_Report_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Report_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_ProjectSalesRegion_Tags_SalesRegionTagId",
                        column: x => x.SalesRegionTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report_BusinessCase_ManufacturingLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturingLocationsTagId = table.Column<int>(nullable: false),
                    BusinessCaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_BusinessCase_ManufacturingLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_BusinessCase_ManufacturingLocations_Report_BusinessCase_BusinessCaseId",
                        column: x => x.BusinessCaseId,
                        principalTable: "Report_BusinessCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_BusinessCase_ManufacturingLocations_Tags_ManufacturingLocationsTagId",
                        column: x => x.ManufacturingLocationsTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report_FinancialData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessCaseId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    StdCostEstimated = table.Column<decimal>(nullable: false),
                    SalesPriceEstimated = table.Column<decimal>(nullable: false),
                    GPACapital = table.Column<decimal>(nullable: true),
                    GPAExpense = table.Column<decimal>(nullable: true),
                    QualCosts = table.Column<decimal>(nullable: true),
                    OtherDevelopmentExpenses = table.Column<decimal>(nullable: true),
                    GetCostExtended = table.Column<decimal>(nullable: false),
                    GetRevenueExtended = table.Column<decimal>(nullable: false),
                    GetStandardMarginPrice = table.Column<decimal>(nullable: false),
                    GetStandardMarginPercent = table.Column<decimal>(nullable: false),
                    GetTotalExpenses = table.Column<decimal>(nullable: false),
                    GetNetProfitBeforeTax = table.Column<decimal>(nullable: false),
                    GetNetProfitAfterTax = table.Column<decimal>(nullable: false),
                    GetFreeCashFlow = table.Column<decimal>(nullable: false),
                    GetPresentValue = table.Column<decimal>(nullable: false),
                    GetCumulativeCashFlow = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_FinancialData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_FinancialData_Report_BusinessCase_BusinessCaseId",
                        column: x => x.BusinessCaseId,
                        principalTable: "Report_BusinessCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ProjectId",
                table: "Report_BusinessCase",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ManufacturingLocations_BusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations",
                column: "BusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ManufacturingLocations_ManufacturingLocationsTagId",
                table: "Report_BusinessCase_ManufacturingLocations",
                column: "ManufacturingLocationsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_FinancialData_BusinessCaseId",
                table: "Report_FinancialData",
                column: "BusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Project_DesignAuthorityTagId",
                table: "Report_Project",
                column: "DesignAuthorityTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Project_ExportApplicationTypeTagId",
                table: "Report_Project",
                column: "ExportApplicationTypeTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Project_ProductLineTagId",
                table: "Report_Project",
                column: "ProductLineTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Project_ProjectCategoryTagId",
                table: "Report_Project",
                column: "ProjectCategoryTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Project_ProjectClassificationTagId",
                table: "Report_Project",
                column: "ProjectClassificationTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Project_ProjectId",
                table: "Report_Project",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectCustomers_CustomersTagId",
                table: "Report_ProjectCustomers",
                column: "CustomersTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectCustomers_ProjectId",
                table: "Report_ProjectCustomers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectEndUserCountries_EndUserCountriesTagId",
                table: "Report_ProjectEndUserCountries",
                column: "EndUserCountriesTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectEndUserCountries_ProjectId",
                table: "Report_ProjectEndUserCountries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectSalesRegion_ProjectId",
                table: "Report_ProjectSalesRegion",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectSalesRegion_SalesRegionTagId",
                table: "Report_ProjectSalesRegion",
                column: "SalesRegionTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report_BusinessCase_ManufacturingLocations");

            migrationBuilder.DropTable(
                name: "Report_FinancialData");

            migrationBuilder.DropTable(
                name: "Report_ProjectCustomers");

            migrationBuilder.DropTable(
                name: "Report_ProjectEndUserCountries");

            migrationBuilder.DropTable(
                name: "Report_ProjectSalesRegion");

            migrationBuilder.DropTable(
                name: "Report_BusinessCase");

            migrationBuilder.DropTable(
                name: "Report_Project");
        }
    }
}
