using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GateConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    GateNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StageNumber = table.Column<int>(nullable: false),
                    AllowInsertRiskAssesments = table.Column<bool>(nullable: false),
                    MinProjectJustifications = table.Column<int>(nullable: true),
                    MinBusinessCases = table.Column<int>(nullable: true),
                    MinProductInfringementPatentabilities = table.Column<int>(nullable: true),
                    MinKeyCharacteristics = table.Column<int>(nullable: true),
                    MinCustomerDesignApprovals = table.Column<int>(nullable: true),
                    MinInvestmentPlans = table.Column<int>(nullable: true),
                    MinProductIntroChecklist = table.Column<int>(nullable: true),
                    MinRampResourcePlans = table.Column<int>(nullable: true),
                    MinQualificationTesting = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    FriendlyName = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    IsFixed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    FriendlyName = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    CanInitiateProject = table.Column<bool>(nullable: false),
                    ManagesPauseProject = table.Column<bool>(nullable: false),
                    ManagesProjectDetail = table.Column<bool>(nullable: false),
                    ManagesProjectTeam = table.Column<bool>(nullable: false),
                    ManagesScheduling = table.Column<bool>(nullable: false),
                    ManagesBusinessCases = table.Column<bool>(nullable: false),
                    ManagesDeliverableRegisters = table.Column<bool>(nullable: false),
                    ManagesIntellectualProperty = table.Column<bool>(nullable: false),
                    ManagesProjectRequirements = table.Column<bool>(nullable: false),
                    ManagesRiskAnalysis = table.Column<bool>(nullable: false),
                    ManagesParts = table.Column<bool>(nullable: false),
                    ManagesCustomerDesignApproval = table.Column<bool>(nullable: false),
                    ManagesInvestmentPlan = table.Column<bool>(nullable: false),
                    ManagesMarketingPlan = table.Column<bool>(nullable: false),
                    ManagesRampAndResourcePlan = table.Column<bool>(nullable: false),
                    ManagesDeliverableRegister = table.Column<bool>(nullable: false),
                    ManagesQualificationTesting = table.Column<bool>(nullable: false),
                    GateConfigId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_GateConfigs_GateConfigId",
                        column: x => x.GateConfigId,
                        principalTable: "GateConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    ActualReviewDate = table.Column<DateTime>(nullable: false),
                    Decision = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gates_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    StageNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TagCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_TagCategories_TagCategoryId",
                        column: x => x.TagCategoryId,
                        principalTable: "TagCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    NetworkUsername = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GateApprovers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    GateApproverName = table.Column<string>(nullable: true),
                    GateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateApprovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateApprovers_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    WillCustomerFundQual = table.Column<bool>(nullable: false),
                    WillCustomerFundTooling = table.Column<bool>(nullable: false),
                    ProbabiltyOfWin = table.Column<decimal>(nullable: false),
                    TargetFirstYearGrossMargin = table.Column<decimal>(nullable: false),
                    DataStartingDate = table.Column<DateTime>(nullable: false),
                    NumberOfYears = table.Column<int>(nullable: false),
                    DiscountRate = table.Column<decimal>(nullable: false),
                    TaxRate = table.Column<decimal>(nullable: false),
                    GpaRequirements = table.Column<string>(nullable: true),
                    MultipleFieldsGeneratedTable = table.Column<decimal>(nullable: false),
                    ProjectScope = table.Column<string>(nullable: true),
                    WorkRequirementAmount = table.Column<decimal>(nullable: false),
                    WorkRequirementUrl = table.Column<string>(nullable: true),
                    StrategicAlignment = table.Column<bool>(nullable: false),
                    AddToTecnicalAbilities = table.Column<string>(nullable: true),
                    ProjectCompletion = table.Column<DateTime>(nullable: false),
                    TimeFromProjectCompletionToRevenueGeneration = table.Column<decimal>(nullable: false),
                    CustomerMarketAnalysis = table.Column<string>(nullable: true),
                    Changes = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCases_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDesignApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    SentForApprovalBy = table.Column<string>(nullable: true),
                    DateSentForApprove = table.Column<DateTime>(nullable: false),
                    ApprovedBy = table.Column<string>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDesignApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDesignApprovals_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    ItemNumber = table.Column<string>(nullable: true),
                    Item = table.Column<string>(nullable: true),
                    PurchasedFrom = table.Column<string>(nullable: true),
                    ShipToLocation = table.Column<string>(nullable: true),
                    Cost = table.Column<string>(nullable: true),
                    Terms = table.Column<string>(nullable: true),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentPlans_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInfrigmentPatentabilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    ContainsInfingmentIssues = table.Column<bool>(nullable: false),
                    PatentNumber = table.Column<string>(nullable: true),
                    Issue = table.Column<string>(nullable: true),
                    MitigationStrategy = table.Column<string>(nullable: true),
                    ContainsFeaturesRequiringIPProtection = table.Column<bool>(nullable: false),
                    InventionDisclosureSubmitted = table.Column<bool>(nullable: false),
                    ProductFirstTimeOfferedForSale = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInfrigmentPatentabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInfrigmentPatentabilities_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductIntroChecklists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    IsMarketingRequired = table.Column<bool>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    ApprovedByDate = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIntroChecklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductIntroChecklists_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QualificationTestings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    IsQualificationRequired = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AttachmentUrl = table.Column<string>(nullable: true),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationTestings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualificationTestings_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RampResourcePlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RampResourcePlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RampResourcePlans_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KeyCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    ItemNumber = table.Column<string>(nullable: true),
                    Requirement = table.Column<string>(nullable: true),
                    RequirementSourceId = table.Column<int>(nullable: false),
                    MeasuredValue = table.Column<string>(nullable: true),
                    ExpectedOutcomeRisk = table.Column<string>(nullable: true),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyCharacteristics_Tags_RequirementSourceId",
                        column: x => x.RequirementSourceId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyCharacteristics_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    SalesforceNumber = table.Column<decimal>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    IsExportConrolRequired = table.Column<bool>(nullable: false),
                    EngineeringChecklistUrl = table.Column<string>(nullable: true),
                    ProjectCategoryTagId = table.Column<int>(nullable: false),
                    ProductLineTagId = table.Column<int>(nullable: false),
                    ProjectClassificationTagId = table.Column<int>(nullable: false),
                    ExportApplicationTypeTagId = table.Column<int>(nullable: false),
                    DesignAuthorityTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Tags_DesignAuthorityTagId",
                        column: x => x.DesignAuthorityTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Tags_ExportApplicationTypeTagId",
                        column: x => x.ExportApplicationTypeTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Tags_ProductLineTagId",
                        column: x => x.ProductLineTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Tags_ProjectCategoryTagId",
                        column: x => x.ProjectCategoryTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Tags_ProjectClassificationTagId",
                        column: x => x.ProjectClassificationTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectJustifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    CustomerMotivation = table.Column<string>(nullable: true),
                    Application = table.Column<string>(nullable: true),
                    DrawingSpecStandards = table.Column<string>(nullable: true),
                    RegulatoryRequirements = table.Column<string>(nullable: true),
                    OtherRequirements = table.Column<string>(nullable: true),
                    TechnicalFeasibility = table.Column<string>(nullable: true),
                    Scope = table.Column<string>(nullable: true),
                    AddToInhouseTechnicalAbilities = table.Column<bool>(nullable: false),
                    ProductTagId = table.Column<int>(nullable: true),
                    Competetion = table.Column<string>(nullable: true),
                    BenchmarkSamples = table.Column<string>(nullable: true),
                    AdvantagesWeOffer = table.Column<string>(nullable: true),
                    WhyOurOfferPreferred = table.Column<string>(nullable: true),
                    SingleUseProduct = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectJustifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectJustifications_Tags_ProductTagId",
                        column: x => x.ProductTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectJustifications_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Risks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RiskPropability = table.Column<decimal>(nullable: false),
                    RiskTypeTagId = table.Column<int>(nullable: false),
                    RiskImpactTagId = table.Column<int>(nullable: false),
                    StageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Risks_Tags_RiskImpactTagId",
                        column: x => x.RiskImpactTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Risks_Tags_RiskTypeTagId",
                        column: x => x.RiskTypeTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Risks_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StageConfig_RequiredDesignConcepts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RequiredDesignConceptTagId = table.Column<int>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "StageConfig_RequiredSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RequiredSchedulesTagId = table.Column<int>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageConfig_RequiredSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageConfig_RequiredSchedules_Tags_RequiredSchedulesTagId",
                        column: x => x.RequiredSchedulesTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageConfig_RequiredSchedules_StageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "StageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project_User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_User_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_User_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCitizenShip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCitizenShip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCitizenShip_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCitizenShip_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCase_ManufacturingLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    ManufacturingLocationsTagId = table.Column<int>(nullable: false),
                    BusinessCaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCase_ManufacturingLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCase_ManufacturingLocations_BusinessCases_BusinessCaseId",
                        column: x => x.BusinessCaseId,
                        principalTable: "BusinessCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessCase_ManufacturingLocations_Tags_ManufacturingLocationsTagId",
                        column: x => x.ManufacturingLocationsTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesignConcepts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    DeliverableRegisterId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    DesignConceptType = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    BusinessCaseId = table.Column<int>(nullable: true),
                    StageId = table.Column<int>(nullable: true)
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
                        name: "FK_DesignConcepts_Tags_DeliverableRegisterId",
                        column: x => x.DeliverableRegisterId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignConcepts_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UploadedDocumentation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    CustomerDesignApprovalId = table.Column<int>(nullable: true),
                    GateId = table.Column<int>(nullable: true),
                    ProductInfrigmentPatentabilityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedDocumentation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedDocumentation_CustomerDesignApprovals_CustomerDesignApprovalId",
                        column: x => x.CustomerDesignApprovalId,
                        principalTable: "CustomerDesignApprovals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadedDocumentation_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadedDocumentation_ProductInfrigmentPatentabilities_ProductInfrigmentPatentabilityId",
                        column: x => x.ProductInfrigmentPatentabilityId,
                        principalTable: "ProductInfrigmentPatentabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetail_Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    CustomersTagId = table.Column<int>(nullable: false),
                    ProjectDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetail_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDetail_Customers_Tags_CustomersTagId",
                        column: x => x.CustomersTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetail_Customers_ProjectDetails_ProjectDetailId",
                        column: x => x.ProjectDetailId,
                        principalTable: "ProjectDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetail_EndUserCountries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    EndUserCountriesTagId = table.Column<int>(nullable: false),
                    ProjectDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetail_EndUserCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDetail_EndUserCountries_Tags_EndUserCountriesTagId",
                        column: x => x.EndUserCountriesTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetail_EndUserCountries_ProjectDetails_ProjectDetailId",
                        column: x => x.ProjectDetailId,
                        principalTable: "ProjectDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetail_SalesRegions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    SalesRegionTagId = table.Column<int>(nullable: false),
                    ProjectDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetail_SalesRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDetail_SalesRegions_ProjectDetails_ProjectDetailId",
                        column: x => x.ProjectDetailId,
                        principalTable: "ProjectDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetail_SalesRegions_Tags_SalesRegionTagId",
                        column: x => x.SalesRegionTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mitigations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StartUpdateDate = table.Column<DateTime>(nullable: false),
                    EndUpdateDate = table.Column<DateTime>(nullable: false),
                    RiskId = table.Column<int>(nullable: false),
                    MitigationPlan = table.Column<string>(nullable: true),
                    Responsibility = table.Column<string>(nullable: true),
                    TargetDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mitigations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mitigations_Risks_RiskId",
                        column: x => x.RiskId,
                        principalTable: "Risks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCase_ManufacturingLocations_BusinessCaseId",
                table: "BusinessCase_ManufacturingLocations",
                column: "BusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCase_ManufacturingLocations_ManufacturingLocationsTagId",
                table: "BusinessCase_ManufacturingLocations",
                column: "ManufacturingLocationsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCases_StageId",
                table: "BusinessCases",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDesignApprovals_StageId",
                table: "CustomerDesignApprovals",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_BusinessCaseId",
                table: "DesignConcepts",
                column: "BusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_DeliverableRegisterId",
                table: "DesignConcepts",
                column: "DeliverableRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_StageId",
                table: "DesignConcepts",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_GateApprovers_GateId",
                table: "GateApprovers",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_Gates_ProjectId",
                table: "Gates",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentPlans_StageId",
                table: "InvestmentPlans",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyCharacteristics_RequirementSourceId",
                table: "KeyCharacteristics",
                column: "RequirementSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyCharacteristics_StageId",
                table: "KeyCharacteristics",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Mitigations_RiskId",
                table: "Mitigations",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfrigmentPatentabilities_StageId",
                table: "ProductInfrigmentPatentabilities",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIntroChecklists_StageId",
                table: "ProductIntroChecklists",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_User_ProjectId",
                table: "Project_User",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_User_UserId",
                table: "Project_User",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_Customers_CustomersTagId",
                table: "ProjectDetail_Customers",
                column: "CustomersTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_Customers_ProjectDetailId",
                table: "ProjectDetail_Customers",
                column: "ProjectDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_EndUserCountries_EndUserCountriesTagId",
                table: "ProjectDetail_EndUserCountries",
                column: "EndUserCountriesTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_EndUserCountries_ProjectDetailId",
                table: "ProjectDetail_EndUserCountries",
                column: "ProjectDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_SalesRegions_ProjectDetailId",
                table: "ProjectDetail_SalesRegions",
                column: "ProjectDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_SalesRegions_SalesRegionTagId",
                table: "ProjectDetail_SalesRegions",
                column: "SalesRegionTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_DesignAuthorityTagId",
                table: "ProjectDetails",
                column: "DesignAuthorityTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ExportApplicationTypeTagId",
                table: "ProjectDetails",
                column: "ExportApplicationTypeTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ProductLineTagId",
                table: "ProjectDetails",
                column: "ProductLineTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ProjectCategoryTagId",
                table: "ProjectDetails",
                column: "ProjectCategoryTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ProjectClassificationTagId",
                table: "ProjectDetails",
                column: "ProjectClassificationTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ProjectId",
                table: "ProjectDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJustifications_ProductTagId",
                table: "ProjectJustifications",
                column: "ProductTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJustifications_StageId",
                table: "ProjectJustifications",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_QualificationTestings_StageId",
                table: "QualificationTestings",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_RampResourcePlans_StageId",
                table: "RampResourcePlans",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Risks_RiskImpactTagId",
                table: "Risks",
                column: "RiskImpactTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Risks_RiskTypeTagId",
                table: "Risks",
                column: "RiskTypeTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Risks_StageId",
                table: "Risks",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_GateConfigId",
                table: "Roles",
                column: "GateConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_StageId",
                table: "Schedules",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TagId",
                table: "Schedules",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredDesignConcepts_RequiredDesignConceptTagId",
                table: "StageConfig_RequiredDesignConcepts",
                column: "RequiredDesignConceptTagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredDesignConcepts_StageConfigId",
                table: "StageConfig_RequiredDesignConcepts",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredSchedules_RequiredSchedulesTagId",
                table: "StageConfig_RequiredSchedules",
                column: "RequiredSchedulesTagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredSchedules_StageConfigId",
                table: "StageConfig_RequiredSchedules",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_ProjectId",
                table: "Stages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagCategoryId",
                table: "Tags",
                column: "TagCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedDocumentation_CustomerDesignApprovalId",
                table: "UploadedDocumentation",
                column: "CustomerDesignApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedDocumentation_GateId",
                table: "UploadedDocumentation",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedDocumentation_ProductInfrigmentPatentabilityId",
                table: "UploadedDocumentation",
                column: "ProductInfrigmentPatentabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCitizenShip_TagId",
                table: "UserCitizenShip",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCitizenShip_UserId",
                table: "UserCitizenShip",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCase_ManufacturingLocations");

            migrationBuilder.DropTable(
                name: "DesignConcepts");

            migrationBuilder.DropTable(
                name: "GateApprovers");

            migrationBuilder.DropTable(
                name: "InvestmentPlans");

            migrationBuilder.DropTable(
                name: "KeyCharacteristics");

            migrationBuilder.DropTable(
                name: "Mitigations");

            migrationBuilder.DropTable(
                name: "ProductIntroChecklists");

            migrationBuilder.DropTable(
                name: "Project_User");

            migrationBuilder.DropTable(
                name: "ProjectDetail_Customers");

            migrationBuilder.DropTable(
                name: "ProjectDetail_EndUserCountries");

            migrationBuilder.DropTable(
                name: "ProjectDetail_SalesRegions");

            migrationBuilder.DropTable(
                name: "ProjectJustifications");

            migrationBuilder.DropTable(
                name: "QualificationTestings");

            migrationBuilder.DropTable(
                name: "RampResourcePlans");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "StageConfig_RequiredDesignConcepts");

            migrationBuilder.DropTable(
                name: "StageConfig_RequiredSchedules");

            migrationBuilder.DropTable(
                name: "UploadedDocumentation");

            migrationBuilder.DropTable(
                name: "UserCitizenShip");

            migrationBuilder.DropTable(
                name: "BusinessCases");

            migrationBuilder.DropTable(
                name: "Risks");

            migrationBuilder.DropTable(
                name: "ProjectDetails");

            migrationBuilder.DropTable(
                name: "StageConfigs");

            migrationBuilder.DropTable(
                name: "CustomerDesignApprovals");

            migrationBuilder.DropTable(
                name: "Gates");

            migrationBuilder.DropTable(
                name: "ProductInfrigmentPatentabilities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TagCategories");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "GateConfigs");
        }
    }
}
