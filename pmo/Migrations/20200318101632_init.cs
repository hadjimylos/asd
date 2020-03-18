using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pmo.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LiteStageConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    StageNumber = table.Column<int>(nullable: false),
                    AllowInsertRiskAssesments = table.Column<bool>(nullable: false),
                    MinProjectJustifications = table.Column<int>(nullable: false),
                    MinBusinessCases = table.Column<int>(nullable: false),
                    MinProductInfringementPatentabilities = table.Column<int>(nullable: false),
                    MinKeyCharacteristics = table.Column<int>(nullable: false),
                    MinCustomerDesignApprovals = table.Column<int>(nullable: false),
                    MinInvestmentPlans = table.Column<int>(nullable: false),
                    MinProductIntroChecklist = table.Column<int>(nullable: false),
                    MinPostLaunchReviews = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteStageConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
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
                    ManagesQualificationTesting = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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
                    MinProjectJustifications = table.Column<int>(nullable: false),
                    MinBusinessCases = table.Column<int>(nullable: false),
                    MinProductInfringementPatentabilities = table.Column<int>(nullable: false),
                    MinKeyCharacteristics = table.Column<int>(nullable: false),
                    MinCustomerDesignApprovals = table.Column<int>(nullable: false),
                    MinInvestmentPlans = table.Column<int>(nullable: false),
                    MinProductIntroChecklist = table.Column<int>(nullable: false),
                    MinPostLaunchReviews = table.Column<int>(nullable: false)
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
                name: "LiteGateKeeperConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Keeper = table.Column<string>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteGateKeeperConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiteGateKeeperConfigs_LiteStageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "LiteStageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStateHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStateHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectStateHistories_Projects_ProjectId",
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
                    ProjectId = table.Column<int>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    NetworkUsername = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
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
                name: "GateKeeperConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Keeper = table.Column<string>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateKeeperConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateKeeperConfigs_StageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "StageConfigs",
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
                    StageConfigId = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Gates_StageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "StageConfigs",
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
                name: "BusinessCases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
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
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    SentForApprovalBy = table.Column<string>(nullable: true),
                    DateSentForApprove = table.Column<DateTime>(nullable: false),
                    ApprovedBy = table.Column<string>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: false)
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
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    ItemNumber = table.Column<string>(nullable: true),
                    Item = table.Column<string>(nullable: true),
                    PurchasedFrom = table.Column<string>(nullable: true),
                    ShipToLocation = table.Column<string>(nullable: true),
                    Cost = table.Column<string>(nullable: true),
                    Terms = table.Column<string>(nullable: true)
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
                name: "PostLaunchReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    DoneWell = table.Column<string>(nullable: true),
                    DonePoorly = table.Column<string>(nullable: true),
                    Bottlenecks = table.Column<string>(nullable: true),
                    LessonsLearned = table.Column<string>(nullable: true),
                    ActualVSExpected = table.Column<string>(nullable: true),
                    Commercial = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLaunchReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostLaunchReviews_Stages_StageId",
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
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    PatentNumber = table.Column<string>(nullable: true),
                    Issue = table.Column<string>(nullable: true),
                    MitigationStrategy = table.Column<string>(nullable: true),
                    ContainsFeaturesRequiringIPProtection = table.Column<bool>(nullable: false),
                    InventionDisclosureSubmitted = table.Column<bool>(nullable: false),
                    ProductFirstTimeOfferedForSale = table.Column<DateTime>(nullable: false)
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
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    ApprovedByUserId = table.Column<int>(nullable: false),
                    ApprovedByDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIntroChecklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductIntroChecklists_Users_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductIntroChecklists_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
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
                    UserId = table.Column<int>(nullable: false),
                    JobDescriptionKey = table.Column<string>(nullable: true)
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
                name: "GateComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    GateId = table.Column<int>(nullable: false),
                    DecisionType = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateComments_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GateFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    GateNumber = table.Column<int>(nullable: false),
                    GateId = table.Column<int>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateFiles_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GateKeeperLites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    GateId = table.Column<int>(nullable: false),
                    GateKeeperConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateKeeperLites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateKeeperLites_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateKeeperLites_LiteGateKeeperConfigs_GateKeeperConfigId",
                        column: x => x.GateKeeperConfigId,
                        principalTable: "LiteGateKeeperConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateKeeperLites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GateKeepers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    GateId = table.Column<int>(nullable: false),
                    GateKeeperConfigId = table.Column<int>(nullable: false),
                    LiteGateKeeperConfigId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateKeepers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateKeepers_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateKeepers_GateKeeperConfigs_GateKeeperConfigId",
                        column: x => x.GateKeeperConfigId,
                        principalTable: "GateKeeperConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateKeepers_LiteGateKeeperConfigs_LiteGateKeeperConfigId",
                        column: x => x.LiteGateKeeperConfigId,
                        principalTable: "LiteGateKeeperConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GateKeepers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    ItemNumber = table.Column<string>(nullable: true),
                    Requirement = table.Column<string>(nullable: true),
                    RequirementSourceId = table.Column<int>(nullable: false),
                    MeasuredValue = table.Column<string>(nullable: true),
                    ExpectedOutcomeRisk = table.Column<string>(nullable: true)
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
                name: "LiteRequiredSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RequiredScheduleTagId = table.Column<int>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteRequiredSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiteRequiredSchedules_Tags_RequiredScheduleTagId",
                        column: x => x.RequiredScheduleTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiteRequiredSchedules_LiteStageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "LiteStageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiteStageFileConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RequiredFileTagId = table.Column<int>(nullable: false),
                    IsLocation = table.Column<bool>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteStageFileConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiteStageFileConfigs_Tags_RequiredFileTagId",
                        column: x => x.RequiredFileTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiteStageFileConfigs_LiteStageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "LiteStageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ProjectDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Salesforce = table.Column<string>(nullable: false),
                    ProjectCategoryTagId = table.Column<int>(nullable: false),
                    ProductLineTagId = table.Column<int>(nullable: false),
                    ProjectClassificationTagId = table.Column<int>(nullable: false),
                    ExportApplicationTypeTagId = table.Column<int>(nullable: false),
                    DesignAuthorityTagId = table.Column<int>(nullable: false),
                    ExportControlCode = table.Column<string>(nullable: false),
                    EndUseDestinationCountry = table.Column<string>(nullable: false),
                    ProjectProcessType = table.Column<string>(nullable: false)
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
                    Version = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    CustomerMotivation = table.Column<string>(nullable: true),
                    Application = table.Column<string>(nullable: true),
                    DrawingSpecStandards = table.Column<string>(nullable: true),
                    RegulatoryRequirements = table.Column<string>(nullable: true),
                    OtherRequirements = table.Column<string>(nullable: true),
                    TechnicalFeasibility = table.Column<string>(nullable: true),
                    Scope = table.Column<string>(nullable: true),
                    AddToInhouseTechnicalAbilitiesTagId = table.Column<int>(nullable: false),
                    Product = table.Column<string>(nullable: true),
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
                        name: "FK_ProjectJustifications_Tags_AddToInhouseTechnicalAbilitiesTagId",
                        column: x => x.AddToInhouseTechnicalAbilitiesTagId,
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
                name: "Report_Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
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
                name: "Risks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
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
                name: "StageConfig_RequiredSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RequiredScheduleTagId = table.Column<int>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageConfig_RequiredSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageConfig_RequiredSchedules_Tags_RequiredScheduleTagId",
                        column: x => x.RequiredScheduleTagId,
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
                name: "StageFileConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RequiredFileTagId = table.Column<int>(nullable: false),
                    IsLocation = table.Column<bool>(nullable: false),
                    StageConfigId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageFileConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageFileConfigs_Tags_RequiredFileTagId",
                        column: x => x.RequiredFileTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageFileConfigs_StageConfigs_StageConfigId",
                        column: x => x.StageConfigId,
                        principalTable: "StageConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StageFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    FileTagId = table.Column<int>(nullable: false),
                    StageId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsLocation = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageFiles_Tags_FileTagId",
                        column: x => x.FileTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageFiles_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
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
                name: "FinancialData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    BusinessCaseId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    StdCostEstimated = table.Column<decimal>(nullable: false),
                    SalesPriceEstimated = table.Column<decimal>(nullable: false),
                    GPACapital = table.Column<decimal>(nullable: true),
                    GPAExpense = table.Column<decimal>(nullable: true),
                    QualCosts = table.Column<decimal>(nullable: true),
                    OtherDevelopmentExpenses = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialData_BusinessCases_BusinessCaseId",
                        column: x => x.BusinessCaseId,
                        principalTable: "BusinessCases",
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
                name: "Report_BusinessCase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportProjectId = table.Column<int>(nullable: false),
                    StageNumber = table.Column<int>(nullable: false),
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
                        name: "FK_Report_BusinessCase_Report_Project_ReportProjectId",
                        column: x => x.ReportProjectId,
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
                    ReportProjectId = table.Column<int>(nullable: false)
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
                        name: "FK_Report_ProjectCustomers_Report_Project_ReportProjectId",
                        column: x => x.ReportProjectId,
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
                    ReportProjectId = table.Column<int>(nullable: false),
                    EndUserCountriesTagId = table.Column<int>(nullable: false)
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
                        name: "FK_Report_ProjectEndUserCountries_Report_Project_ReportProjectId",
                        column: x => x.ReportProjectId,
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
                    ReportProjectId = table.Column<int>(nullable: false),
                    SalesRegionTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_ProjectSalesRegion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_ProjectSalesRegion_Report_Project_ReportProjectId",
                        column: x => x.ReportProjectId,
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
                name: "Mitigations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedByUser = table.Column<string>(nullable: true),
                    RiskId = table.Column<int>(nullable: false),
                    MitigationPlan = table.Column<string>(nullable: false),
                    ResponsibilityUserId = table.Column<int>(nullable: false),
                    TargetDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mitigations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mitigations_Users_ResponsibilityUserId",
                        column: x => x.ResponsibilityUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mitigations_Risks_RiskId",
                        column: x => x.RiskId,
                        principalTable: "Risks",
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
                    ReportBusinessCaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_BusinessCase_ManufacturingLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_BusinessCase_ManufacturingLocations_Tags_ManufacturingLocationsTagId",
                        column: x => x.ManufacturingLocationsTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_BusinessCase_ManufacturingLocations_Report_BusinessCase_ReportBusinessCaseId",
                        column: x => x.ReportBusinessCaseId,
                        principalTable: "Report_BusinessCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report_FinancialData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportBusinessCaseId = table.Column<int>(nullable: false),
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
                    GetStandardMarginPercent = table.Column<decimal>(nullable: true),
                    GetTotalExpenses = table.Column<decimal>(nullable: false),
                    GetNetProfitBeforeTax = table.Column<decimal>(nullable: false),
                    GetNetProfitAfterTax = table.Column<decimal>(nullable: false),
                    GetFreeCashFlow = table.Column<decimal>(nullable: false),
                    GetPresentValue = table.Column<decimal>(nullable: true),
                    GetCumulativeCashFlow = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_FinancialData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_FinancialData_Report_BusinessCase_ReportBusinessCaseId",
                        column: x => x.ReportBusinessCaseId,
                        principalTable: "Report_BusinessCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "LiteStageConfigs",
                columns: new[] { "Id", "AllowInsertRiskAssesments", "CreateDate", "MinBusinessCases", "MinCustomerDesignApprovals", "MinInvestmentPlans", "MinKeyCharacteristics", "MinPostLaunchReviews", "MinProductInfringementPatentabilities", "MinProductIntroChecklist", "MinProjectJustifications", "ModifiedByUser", "StageNumber" },
                values: new object[,]
                {
                    { 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, 0, 0, 0, 0, 0, 1, "system", 1 },
                    { 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, 1, 0, 0, 0, 1, 0, "system", 2 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanInitiateProject", "CreateDate", "FriendlyName", "IsAdmin", "Key", "ManagesBusinessCases", "ManagesCustomerDesignApproval", "ManagesDeliverableRegister", "ManagesDeliverableRegisters", "ManagesIntellectualProperty", "ManagesInvestmentPlan", "ManagesMarketingPlan", "ManagesParts", "ManagesPauseProject", "ManagesProjectDetail", "ManagesProjectRequirements", "ManagesProjectTeam", "ManagesQualificationTesting", "ManagesRampAndResourcePlan", "ManagesRiskAnalysis", "ManagesScheduling", "ModifiedByUser" },
                values: new object[,]
                {
                    { 1, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System Admin", true, "system-admin", true, true, true, false, true, true, true, true, true, true, true, true, true, true, true, true, "system" },
                    { 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", false, "user", true, true, true, false, true, true, true, true, true, true, true, true, true, true, true, true, "system" }
                });

            migrationBuilder.InsertData(
                table: "StageConfigs",
                columns: new[] { "Id", "AllowInsertRiskAssesments", "CreateDate", "MinBusinessCases", "MinCustomerDesignApprovals", "MinInvestmentPlans", "MinKeyCharacteristics", "MinPostLaunchReviews", "MinProductInfringementPatentabilities", "MinProductIntroChecklist", "MinProjectJustifications", "ModifiedByUser", "StageNumber" },
                values: new object[,]
                {
                    { 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, 0, 0, 0, 0, 0, 1, "system", 1 },
                    { 2, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, 0, 0, 0, 0, 0, 0, "system", 2 },
                    { 3, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1, 1, 0, 1, 1, 0, "system", 3 },
                    { 4, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 0, 0, 0, 0, 0, 0, "system", 4 },
                    { 5, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, 0, 0, 1, 0, 0, 0, "system", 5 }
                });

            migrationBuilder.InsertData(
                table: "TagCategories",
                columns: new[] { "Id", "CreateDate", "FriendlyName", "IsFixed", "Key", "ModifiedByUser" },
                values: new object[,]
                {
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stage Files", false, "stage-files", null },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Export Application Type", true, "export-application-type", null },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design Authority", true, "design-authority", null },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Risk Impact", true, "risk-impact", null },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Risk Probability", true, "risk-probability", null },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Risk Type", true, "risk-type", null },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Product Line", true, "product-line", null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project Classification", true, "project-classification", null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturing Locations", true, "manufacturing-locations", null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Requirement Source", true, "requirement-source", null },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technical Capabilities", false, "technical-capabilities", null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project Category", true, "project-category", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sales Region", true, "sales-region", null },
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Required Schedules", true, "required-schedules", null },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customers", true, "customers", null },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Application Components", true, "application-components", null }
                });

            migrationBuilder.InsertData(
                table: "GateKeeperConfigs",
                columns: new[] { "Id", "CreateDate", "Keeper", "ModifiedByUser", "StageConfigId" },
                values: new object[,]
                {
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Manufacturing Site", "system", 3 },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Product Management/Marketing", "system", 2 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Engineering", "system", 2 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU GM", "system", 2 },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Controller", "system", 2 },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Manufacturing Site", "system", 2 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Product Management/Marketing", "system", 3 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Engineering", "system", 1 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Engineering", "system", 3 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Controller", "system", 3 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Product Management/Marketing", "system", 4 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Engineering", "system", 4 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU GM", "system", 4 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Controller", "system", 4 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Manufacturing Site", "system", 4 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU GM", "system", 3 },
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Product Management/Marketing", "system", 1 }
                });

            migrationBuilder.InsertData(
                table: "LiteGateKeeperConfigs",
                columns: new[] { "Id", "CreateDate", "Keeper", "ModifiedByUser", "StageConfigId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Product Management/Marketing", "system", 1 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Controller", "system", 2 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Controller", "system", 1 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Manufacturing Site", "system", 1 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU VBPD Champion / Facilitator", "system", 1 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Product Management/Marketing", "system", 2 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Engineering", "system", 2 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU GM", "system", 2 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU GM", "system", 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Engineering", "system", 1 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU VBPD Champion / Facilitator", "system", 2 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BU Director Manufacturing Site", "system", 2 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "Name", "TagCategoryId" },
                values: new object[,]
                {
                    { 594, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CCI", 8 },
                    { 593, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thrane & Thrane", 8 },
                    { 592, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Manz Automation", 8 },
                    { 595, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baer Cargolift GmbH", 8 },
                    { 591, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sierra Nevada Corp", 8 },
                    { 590, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Friand", 8 },
                    { 589, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "B&K Medical", 8 },
                    { 576, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "All Powerlock", 8 },
                    { 596, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trane & Trane", 8 },
                    { 597, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Standard Vision", 8 },
                    { 598, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Aircell", 8 },
                    { 599, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "YuChai", 8 },
                    { 600, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "North Atlantic Ind.", 8 },
                    { 601, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Richard Halm GmbH", 8 },
                    { 602, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Global European Market", 8 },
                    { 588, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Advantech", 8 },
                    { 587, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "QinetiQ", 8 },
                    { 581, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Agusta", 8 },
                    { 585, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Intelligent Textiles", 8 },
                    { 577, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "COSL", 8 },
                    { 574, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rolec", 8 },
                    { 573, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chargemaster", 8 },
                    { 572, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Elektromotive", 8 },
                    { 571, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pod Point", 8 },
                    { 570, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kawazaki", 8 },
                    { 586, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Changan UK R&D Ltd", 8 },
                    { 603, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cisco", 8 },
                    { 579, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Airbus", 8 },
                    { 580, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Eurocopter", 8 },
                    { 575, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GE", 8 },
                    { 582, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alenia", 8 },
                    { 583, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "VT Miltope", 8 },
                    { 584, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ZHENGZHOU YUTONG BUS", 8 },
                    { 578, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xioptics", 8 },
                    { 604, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Avnet", 8 },
                    { 619, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kontron", 8 },
                    { 606, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dynamics", 8 },
                    { 626, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AGCO Sisu", 8 },
                    { 627, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonoscape China", 8 },
                    { 628, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Domatech", 8 },
                    { 629, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Toro", 8 },
                    { 630, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "INDU", 8 },
                    { 631, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LEX", 8 },
                    { 632, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Carlisle", 8 },
                    { 633, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Applied Research Associates", 8 },
                    { 634, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Applied Research Associates Inc", 8 },
                    { 635, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Simplified Energy Solutions", 8 },
                    { 636, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NAVAIR", 8 },
                    { 637, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deutz AG", 8 },
                    { 638, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonoscape", 8 },
                    { 639, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CRRC", 8 },
                    { 640, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Zaptec", 8 },
                    { 625, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Husqvarna", 8 },
                    { 605, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "UTC", 8 },
                    { 624, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Parker Hannifin", 8 },
                    { 622, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Palladium", 8 },
                    { 607, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Heinzmann", 8 },
                    { 608, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ford", 8 },
                    { 609, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Turkey", 8 },
                    { 610, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Entwistle", 8 },
                    { 611, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Electro-Wire", 8 },
                    { 612, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AXPO POWER AG", 8 },
                    { 613, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NL1748    :BELKO INKOOPOPDRACHT", 8 },
                    { 614, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "kassbohrer", 8 },
                    { 615, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Persistent", 8 },
                    { 616, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dongfeng", 8 },
                    { 617, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Belko", 8 },
                    { 618, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "WuXi Kailong", 8 },
                    { 569, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suzuki", 8 },
                    { 620, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thales", 8 },
                    { 621, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Boeing", 8 },
                    { 623, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Govt", 8 },
                    { 568, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Yamaha", 8 },
                    { 553, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "THSI", 8 },
                    { 566, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Porsche", 8 },
                    { 513, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EATON AEROSPACE", 8 },
                    { 514, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ford", 8 },
                    { 515, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fuji Heavy Industries", 8 },
                    { 516, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GOODRICH", 8 },
                    { 517, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bobcat", 8 },
                    { 518, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "HARRIS ASSEMBLY", 8 },
                    { 519, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hitachi Medical", 8 },
                    { 520, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Indra Systems Defense", 8 },
                    { 521, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "JTRS", 8 },
                    { 522, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Knorr-Bremse", 8 },
                    { 523, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Leviton", 8 },
                    { 524, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LORAL SPACE", 8 },
                    { 525, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MAN Commercial", 8 },
                    { 526, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MAN Military", 8 },
                    { 527, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Moog", 8 },
                    { 512, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DUCOMMUN TECHNOLOGIES", 8 },
                    { 528, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PENTAIR  WATER  EMEA", 8 },
                    { 511, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Danaher", 8 },
                    { 509, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Caterpillar", 8 },
                    { 494, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SAAB", 8 },
                    { 495, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SCHLUMBERGER ENERGY", 8 },
                    { 496, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SECHERON", 8 },
                    { 497, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SEPSA", 8 },
                    { 498, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SEWS CABIND", 8 },
                    { 499, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sumitomo", 8 },
                    { 500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tektronix", 8 },
                    { 501, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TELEINDUSTRIALE SRL", 8 },
                    { 502, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Textron Bell", 8 },
                    { 503, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Textron Defense", 8 },
                    { 504, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TOSHIBA INDUSTRIAL", 8 },
                    { 505, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TOSHIBA MILITARY", 8 },
                    { 506, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "VOLVO", 8 },
                    { 507, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AREVA", 8 },
                    { 508, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AVIO SPA", 8 },
                    { 510, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CNH", 8 },
                    { 567, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EV Manufacturers", 8 },
                    { 529, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "POD POINT", 8 },
                    { 531, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Shiny Success", 8 },
                    { 551, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tyco Thermal Controls", 8 },
                    { 552, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Saudi Aramco", 8 },
                    { 641, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Unumotors", 8 },
                    { 554, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dunkermotoren", 8 },
                    { 555, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Teldat", 8 },
                    { 556, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Panasonic", 8 },
                    { 557, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Westen Digital", 8 },
                    { 558, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cummins Allison", 8 },
                    { 559, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liebherr Germany", 8 },
                    { 560, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dspace", 8 },
                    { 561, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TVO Finland", 8 },
                    { 562, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Peugeot Citreon - France", 8 },
                    { 563, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Agco - Sisu Power", 8 },
                    { 564, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ITT ICS", 8 },
                    { 565, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Inteligent design cycles", 8 },
                    { 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AWE Aldermaston", 8 },
                    { 530, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ROCKWELL AUTOMATION", 8 },
                    { 549, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "J+B Aviation Services", 8 },
                    { 547, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Total", 8 },
                    { 532, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Singarpore Technologies", 8 },
                    { 533, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tesla", 8 },
                    { 534, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "WEBASTO", 8 },
                    { 535, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Various", 8 },
                    { 536, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AITech", 8 },
                    { 537, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AMK", 8 },
                    { 538, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emitec", 8 },
                    { 539, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Furukawa Electric ( NISSAN)", 8 },
                    { 540, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BRC Compressors", 8 },
                    { 541, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cummins", 8 },
                    { 542, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "KEIHIN", 8 },
                    { 543, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Universal Avionics", 8 },
                    { 544, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nexans Cabling Solutions", 8 },
                    { 545, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wabtec", 8 },
                    { 546, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ENI", 8 },
                    { 548, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cameron Romania", 8 },
                    { 642, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AddEnergie", 8 },
                    { 656, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Telefonix", 8 },
                    { 644, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GN Resound", 8 },
                    { 740, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lainate", 13 },
                    { 741, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Irvine", 13 },
                    { 768, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Santa Ana", 13 },
                    { 742, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Space Application", 14 },
                    { 743, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Military Application", 14 },
                    { 791, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nuclear Application", 14 },
                    { 739, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Shenzhen", 13 },
                    { 792, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Commercial Aerospace Application", 14 },
                    { 794, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "None of the Above", 14 },
                    { 744, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Business Case - Work Required", 15 },
                    { 745, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Design Concepts", 15 },
                    { 746, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - DMFEA", 15 },
                    { 747, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - DVT Test Plan", 15 },
                    { 748, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Qual Test Plan", 15 },
                    { 793, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Commercial Items in Defense Application", 14 },
                    { 738, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Zama", 13 },
                    { 737, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Weinstadt", 13 },
                    { 736, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Basingstoke", 13 },
                    { 721, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Resource", 10 },
                    { 722, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Quality", 10 },
                    { 723, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Others", 10 },
                    { 729, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "10%", 11 },
                    { 730, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "30%", 11 },
                    { 731, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "50%", 11 },
                    { 732, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "70%", 11 },
                    { 733, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "90%", 11 },
                    { 724, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "1", 12 },
                    { 725, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2", 12 },
                    { 726, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "3", 12 },
                    { 727, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "4", 12 },
                    { 728, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5", 12 },
                    { 734, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Santa Rosa", 13 },
                    { 735, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Watertown", 13 },
                    { 749, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Design Review", 15 },
                    { 750, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - PFMEA", 15 },
                    { 751, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - ESH Checklist", 15 },
                    { 752, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Product Safety Analysis", 15 },
                    { 777, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Εngineering Checklist", 15 },
                    { 769, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "None", 16 },
                    { 770, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Some", 16 },
                    { 771, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Significant", 16 },
                    { 778, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Risk assessment", 17 },
                    { 779, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Business Case", 17 },
                    { 780, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Schedules", 17 },
                    { 781, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer Design Approval", 17 },
                    { 782, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Investment Plan", 17 },
                    { 783, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Key Characteristics", 17 },
                    { 784, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post Launch Review", 17 },
                    { 785, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Production Infringement Patentability", 17 },
                    { 786, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Product Intro Checklist", 17 },
                    { 787, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Project Justification", 17 },
                    { 788, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ramp Resource Plan", 17 },
                    { 776, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - FAI Approval", 15 },
                    { 720, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Scope", 10 },
                    { 775, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Control Plan", 15 },
                    { 773, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - CAS Scoping Document", 15 },
                    { 753, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Packaging Definition", 15 },
                    { 754, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Product Drawings ", 15 },
                    { 755, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Specifications", 15 },
                    { 756, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Customer Drawings", 15 },
                    { 757, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Tool Drawings", 15 },
                    { 758, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Process Flow Diagram", 15 },
                    { 759, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Routers", 15 },
                    { 760, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Process Documentation", 15 },
                    { 761, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - MSA", 15 },
                    { 762, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - FAI", 15 },
                    { 763, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Product Infrigment Patentability - Upload any pertinent documentation", 15 },
                    { 764, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer Design Approval - Upload any pertinent documentation", 15 },
                    { 765, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ramp and Resource Plan", 15 },
                    { 766, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Qualification Testing", 15 },
                    { 772, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - Parts List", 15 },
                    { 774, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Deliverable Register - DFMEA", 15 },
                    { 719, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Schedule", 10 },
                    { 718, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cost", 10 },
                    { 717, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Complex Cable Assembly", 9 },
                    { 664, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Evercharge", 8 },
                    { 665, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nedtrain", 8 },
                    { 666, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hyundai", 8 },
                    { 667, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tesla/Daimler/Volvo", 8 },
                    { 668, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Inventus", 8 },
                    { 669, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "New Warrior PEO Project Office", 8 },
                    { 670, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Broad Market", 8 },
                    { 671, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2mm", 9 },
                    { 672, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "38999 Series I, II, and III", 9 },
                    { 673, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AD", 9 },
                    { 674, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Audio", 9 },
                    { 675, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BIW", 9 },
                    { 676, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CA Bayonet", 9 },
                    { 677, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CIC", 9 },
                    { 678, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CLC", 9 },
                    { 663, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "microsoft", 8 },
                    { 679, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CMX", 9 },
                    { 662, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bren-tronics", 8 },
                    { 660, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Digital Receiver Technology", 8 },
                    { 645, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Koni US", 8 },
                    { 646, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bebro", 8 },
                    { 647, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DKEA US", 8 },
                    { 648, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IUSA S.A. de C.V", 8 },
                    { 649, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Elbilgrossisten", 8 },
                    { 650, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Technovative", 8 },
                    { 651, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mitel", 8 },
                    { 652, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Persistent Systems", 8 },
                    { 653, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Velodyne", 8 },
                    { 654, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Aurora-Byton", 8 },
                    { 655, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Drive AI", 8 },
                    { 493, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "RUAG", 8 },
                    { 657, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Innoveering", 8 },
                    { 658, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Aselsan", 8 },
                    { 659, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cotzworks", 8 },
                    { 661, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NA", 8 },
                    { 643, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Elmec", 8 },
                    { 680, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "D Sub", 9 },
                    { 682, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fakra", 9 },
                    { 702, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SLE", 9 },
                    { 703, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Space & Specials", 9 },
                    { 704, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trident", 9 },
                    { 705, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trinity MKJ", 9 },
                    { 706, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Universal Contact", 9 },
                    { 707, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Veam CIR, VBN, Other", 9 },
                    { 708, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Veam J1772", 9 },
                    { 709, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Veam M12", 9 },
                    { 710, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Veam Powerlock & Snaplock", 9 },
                    { 711, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Veam PT", 9 },
                    { 712, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Vector/APD/Harness/Others", 9 },
                    { 713, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wireless", 9 },
                    { 714, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "New Technology", 9 },
                    { 715, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CTC", 9 },
                    { 716, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "VEAM VRPC", 9 },
                    { 701, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SLC", 9 },
                    { 681, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DL", 9 },
                    { 700, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "RF75", 9 },
                    { 698, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rack & Panel", 9 },
                    { 683, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fiber Optics", 9 },
                    { 684, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Filters", 9 },
                    { 685, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hermetics", 9 },
                    { 686, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "High Frequency Specials", 9 },
                    { 687, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MDM", 9 },
                    { 688, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Metr1X", 9 },
                    { 689, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Micros", 9 },
                    { 690, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MIL-DTL 26482 Series I & Mil Battery", 9 },
                    { 691, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MIL-DTL 5015 Firewall", 9 },
                    { 692, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MIL-DTL 5015 Series I", 9 },
                    { 693, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mini RF", 9 },
                    { 694, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nemesis", 9 },
                    { 695, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PCMCIA/CF", 9 },
                    { 696, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PV/CV/KPD", 9 },
                    { 697, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "QLC", 9 },
                    { 699, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "RF50", 9 },
                    { 492, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Row 44", 8 },
                    { 477, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liebherr", 8 },
                    { 490, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Robert Bosch", 8 },
                    { 291, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Brugg", 8 },
                    { 292, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ZF", 8 },
                    { 293, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Patria", 8 },
                    { 294, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Telex", 8 },
                    { 295, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ITT Water & Wastewater", 8 },
                    { 296, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mitsubishi Heavy Industry", 8 },
                    { 290, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MAN", 8 },
                    { 297, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NEC (UAV market)", 8 },
                    { 299, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hyundai Heavy Vehicle", 8 },
                    { 300, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BYD", 8 },
                    { 301, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Foton", 8 },
                    { 302, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ST aerospace", 8 },
                    { 303, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bharat Electronics Ltd", 8 },
                    { 304, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hindustan Aeronautics Ltd", 8 },
                    { 298, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rotem", 8 },
                    { 305, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer z", 8 },
                    { 289, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Komatsu", 8 },
                    { 287, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Indramat", 8 },
                    { 273, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Eibit", 8 },
                    { 274, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sagem", 8 },
                    { 275, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "KMW", 8 },
                    { 276, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liebeherr", 8 },
                    { 277, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Voith", 8 },
                    { 278, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MTU", 8 },
                    { 288, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fokker", 8 },
                    { 279, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FAW", 8 },
                    { 281, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "John Deere", 8 },
                    { 282, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Catepillar", 8 },
                    { 283, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kawaski Rail", 8 },
                    { 284, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Airbus", 8 },
                    { 285, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Schneider", 8 },
                    { 286, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Siemens Industrial", 8 },
                    { 280, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sany", 8 },
                    { 306, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "John Deere/FPT/Market", 8 },
                    { 307, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GD Mercury 2", 8 },
                    { 308, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ZTE", 8 },
                    { 328, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lucent Qingdao", 8 },
                    { 329, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gulfstream", 8 },
                    { 330, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Smiths Detection", 8 },
                    { 331, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Smiths Det", 8 },
                    { 332, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bosch Rexroth", 8 },
                    { 333, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Selex Luton", 8 },
                    { 327, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SYT", 8 },
                    { 334, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lowara", 8 },
                    { 336, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BOMBARDIER SW", 8 },
                    { 337, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Harris", 8 },
                    { 338, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GE Healthcare", 8 },
                    { 339, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Philips", 8 },
                    { 491, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ROCKWELL AUTOMATION", 8 },
                    { 341, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Medison", 8 },
                    { 335, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GDEB", 8 },
                    { 326, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Invensys", 8 },
                    { 325, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IVECO", 8 },
                    { 324, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fiat Power Train", 8 },
                    { 309, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alcatel Lucent", 8 },
                    { 310, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Multipulse", 8 },
                    { 311, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ANSALDOBREDA", 8 },
                    { 312, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GE HC", 8 },
                    { 313, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lucent", 8 },
                    { 314, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AUTS", 8 },
                    { 315, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "optional:", 8 },
                    { 316, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "STEC", 8 },
                    { 317, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CL Led-PRO", 8 },
                    { 318, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alcatel Shanghai Bell", 8 },
                    { 319, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Syracuse", 8 },
                    { 320, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Viasat", 8 },
                    { 321, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "All", 8 },
                    { 322, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GD", 8 },
                    { 323, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NSN", 8 },
                    { 272, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EADS Defense", 8 },
                    { 271, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GE Aviation", 8 },
                    { 270, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Diehl", 8 },
                    { 269, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rheinmatel", 8 },
                    { 219, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Minor Modification", 4 },
                    { 220, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Major Modification", 4 },
                    { 221, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "New to World", 4 },
                    { 222, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "New to ITT", 4 },
                    { 223, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Broad Market ", 5 },
                    { 224, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer Specific (No Marketing Required)", 5 },
                    { 218, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Global", 3 },
                    { 225, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer Specific (With Marketing Support)", 5 },
                    { 226, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cust Dwg", 6 },
                    { 227, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cust Spec", 6 },
                    { 228, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Internal Dwg", 6 },
                    { 229, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Internal Spec", 6 },
                    { 230, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Industry Standard", 6 },
                    { 231, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Best In Class Performance", 6 },
                    { 767, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer Specific", 5 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "Name", "TagCategoryId" },
                values: new object[,]
                {
                    { 217, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Europe", 3 },
                    { 216, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Asia", 3 },
                    { 215, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Americas", 3 },
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gate 1", 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Design concept(s)", 1 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gate 2 / Gate A", 1 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Draft manufacturing drawings", 1 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Design Review", 1 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Submit PAR", 1 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Release Product Documentation", 1 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Create Samples / Prototypes", 1 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DVT testing & Test Report", 1 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gate 3", 1 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "First Article Approval", 1 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Qualification Testing & Test Report", 1 },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer Approval / Release", 1 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gate 4 / Gate B", 1 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PLR Review", 1 },
                    { 232, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Other", 6 },
                    { 342, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hitachi", 8 },
                    { 233, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Irvine", 7 },
                    { 235, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nogales", 7 },
                    { 255, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hamilton Sundstrand", 8 },
                    { 256, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Oshkosh", 8 },
                    { 257, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DRS", 8 },
                    { 258, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GE Medical", 8 },
                    { 259, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Motorola", 8 },
                    { 260, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "RIM", 8 },
                    { 254, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "General Atomics", 8 },
                    { 261, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Haliburton", 8 },
                    { 263, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rockwell Collins", 8 },
                    { 264, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Siemens", 8 },
                    { 265, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cobham", 8 },
                    { 266, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ultra", 8 },
                    { 267, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Esterline Wallop", 8 },
                    { 268, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Racal", 8 },
                    { 262, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Honeywell Aerospace", 8 },
                    { 253, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alstom", 8 },
                    { 252, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Finmeccanica", 8 },
                    { 251, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MBDA", 8 },
                    { 236, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Weinstadt", 7 },
                    { 237, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Zarma", 7 },
                    { 238, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Shenzhen", 7 },
                    { 239, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ITT - ICS (R&D)", 8 },
                    { 240, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ITT Defense", 8 },
                    { 241, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Boeing", 8 },
                    { 242, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bombardier Rail", 8 },
                    { 243, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bombardier Commercial Air", 8 },
                    { 244, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "General Dynamics", 8 },
                    { 245, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Raytheon", 8 },
                    { 246, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Northrop", 8 },
                    { 247, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Textron", 8 },
                    { 248, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lockheed Martin", 8 },
                    { 249, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BAE", 8 },
                    { 250, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thales", 8 },
                    { 234, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lainate", 7 },
                    { 343, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mindray", 8 },
                    { 340, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ESAOTE", 8 },
                    { 345, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bathium", 8 },
                    { 439, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ST Electronics", 8 },
                    { 440, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Milper", 8 },
                    { 441, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Amphenol", 8 },
                    { 442, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lucy Switchgear ltd", 8 },
                    { 443, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "INTELEGENT TEXTILES", 8 },
                    { 444, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suzlon", 8 },
                    { 438, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orbital", 8 },
                    { 445, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TBD", 8 },
                    { 447, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Boeing/Labinal", 8 },
                    { 448, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ABB", 8 },
                    { 449, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AEROVIRONMENT INC", 8 },
                    { 450, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ALLISON", 8 },
                    { 451, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ALOKA", 8 },
                    { 452, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AMA SPA TRANS", 8 },
                    { 446, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Boeing/Fokker Elmo", 8 },
                    { 437, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MOBA", 8 },
                    { 436, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Danfoss", 8 },
                    { 435, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BYD", 8 },
                    { 420, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Carlisle Interconnect Technologies", 8 },
                    { 421, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Protronex", 8 },
                    { 344, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "China local", 8 },
                    { 423, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SEER TECHNOLOGY", 8 },
                    { 424, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Daimler AG", 8 },
                    { 425, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Proteras", 8 },
                    { 426, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NEC TOSHIBA SPACE SYSTEM", 8 },
                    { 427, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OSA Industries", 8 },
                    { 428, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "KHI Japan", 8 },
                    { 429, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TEDER - Israel", 8 },
                    { 430, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Calgary Transit", 8 },
                    { 431, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gore UK", 8 },
                    { 432, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PEI Genesis", 8 },
                    { 433, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Starling", 8 },
                    { 434, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pentair", 8 },
                    { 453, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Apple", 8 },
                    { 419, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Loral Space Systems", 8 },
                    { 454, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ARROW", 8 },
                    { 456, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BENCHMARK", 8 },
                    { 476, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "KONGSBERG", 8 },
                    { 789, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Qualification Testing", 17 },
                    { 478, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MTU", 8 },
                    { 479, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MULTI PHASE METERS AS", 8 },
                    { 480, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NEC", 8 },
                    { 481, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "OECO", 8 },
                    { 475, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IVECO TRUCK/BUS", 8 },
                    { 482, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orbital", 8 },
                    { 484, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Oshkosh Transport", 8 },
                    { 485, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PHILIPS MEDICAL", 8 },
                    { 486, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PKC", 8 },
                    { 487, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PREMIER CABLES", 8 },
                    { 488, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PRINOTH", 8 },
                    { 489, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rheinmetall", 8 },
                    { 483, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Oshkosh Military", 8 },
                    { 474, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ITT F&Motion Control", 8 },
                    { 473, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Halliburton", 8 },
                    { 472, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GE RAIL", 8 },
                    { 457, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BOSCH", 8 },
                    { 458, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "COBO TRANS", 8 },
                    { 459, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CUMMINS TRUCK/BUS", 8 },
                    { 460, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DAIMLER", 8 },
                    { 461, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DELPHI", 8 },
                    { 462, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EADS/Airbus", 8 },
                    { 463, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Elbit", 8 },
                    { 464, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ELENSYS S.R.L.", 8 },
                    { 465, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Embraer", 8 },
                    { 466, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "EMITEC", 8 },
                    { 467, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FAIVELEY RAIL", 8 },
                    { 468, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "FANUC", 8 },
                    { 469, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Faradyne Motors (Suzhou)", 8 },
                    { 470, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GE Energy", 8 },
                    { 471, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GE INDUSTRIAL", 8 },
                    { 455, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BARCO", 8 },
                    { 418, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "L3", 8 },
                    { 422, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "JRC (Toshiba)", 8 },
                    { 416, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Multipulse Network Rail", 8 },
                    { 365, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Honda", 8 },
                    { 366, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IEC sales Europe", 8 },
                    { 367, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IEC sales Asia", 8 },
                    { 368, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IEC sales US", 8 },
                    { 369, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hua Chuang", 8 },
                    { 370, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GD/Rockwell", 8 },
                    { 364, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nissan", 8 },
                    { 371, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Catalog Product", 8 },
                    { 373, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tata Leoni, Beijing Benefit, …", 8 },
                    { 374, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Beijing Benefit, …", 8 },
                    { 375, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MTU Friedrichshafen", 8 },
                    { 417, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Toko-electronics", 8 },
                    { 377, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thales TME", 8 },
                    { 378, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ATM MI - Italy", 8 },
                    { 372, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "-", 8 },
                    { 363, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nissan Export", 8 },
                    { 362, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mitsubishi", 8 },
                    { 361, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nissan domestic", 8 },
                    { 346, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DRS-RSTA", 8 },
                    { 347, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bombardier, Alstom, etc", 8 },
                    { 348, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "WIKA", 8 },
                    { 349, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ANSALDO BREDA", 8 },
                    { 350, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Viasystem", 8 },
                    { 351, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Vissysten", 8 },
                    { 352, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Market", 8 },
                    { 353, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alstom", 8 },
                    { 354, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bombardier", 8 },
                    { 355, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "others", 8 },
                    { 356, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "others (railway)", 8 },
                    { 357, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "other market", 8 },
                    { 358, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rail customers", 8 },
                    { 359, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Off Road customers", 8 },
                    { 360, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Industrial customers", 8 },
                    { 379, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kässbohrer", 8 },
                    { 380, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bombardier / Honeywell", 8 },
                    { 376, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bosch Hallein", 8 },
                    { 382, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Distry Package", 8 },
                    { 402, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GA", 8 },
                    { 381, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Honeywell and 2 others", 8 },
                    { 404, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "STEC-Malaysia", 8 },
                    { 405, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IVECO/ASTRA", 8 },
                    { 406, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "KHI", 8 },
                    { 407, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ITT FPS", 8 },
                    { 401, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "19-way", 8 },
                    { 408, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Novatel", 8 },
                    { 410, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Atlantic Tele", 8 },
                    { 411, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ITT", 8 },
                    { 412, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GD Canada", 8 },
                    { 413, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rockwell", 8 },
                    { 414, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BAE Archer", 8 },
                    { 415, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Customer A", 8 },
                    { 409, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Milectria", 8 },
                    { 400, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "7-way", 8 },
                    { 403, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CAF", 8 },
                    { 398, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "4-way", 8 },
                    { 383, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2 projects India", 8 },
                    { 384, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MWM Project", 8 },
                    { 385, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liebherr Nenzing", 8 },
                    { 399, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "6-way", 8 },
                    { 387, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Scomi", 8 },
                    { 388, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pektron", 8 },
                    { 389, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Zhengzhou ShiQi", 8 },
                    { 386, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Qingdao Si Fang", 8 },
                    { 391, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Boeing/MB", 8 },
                    { 390, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Thales Colombe", 8 },
                    { 397, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rafael", 8 },
                    { 396, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ITT/IS", 8 },
                    { 790, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Design Concept", 17 },
                    { 394, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ANSADLOBREDA", 8 },
                    { 393, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AWE", 8 },
                    { 392, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sews", 8 },
                    { 395, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ITT Defence", 8 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "DisplayName", "ModifiedByUser", "NetworkUsername", "RoleId" },
                values: new object[,]
                {
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "system", "global\\george.karagiannakis", 1 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "system", "global\\ioannis.giannakop", 1 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "system", "global\\konstantinos.marolac", 1 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "system", "global\\georgia.bogri", 1 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "system", "global\\christoph.hadjimylo", 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "system", "global\\efthimios.dellis", 1 },
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "system", "global\\georgia.kalyva", 1 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "system", "global\\christos.zaragkidis", 1 }
                });

            migrationBuilder.InsertData(
                table: "LiteRequiredSchedules",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "RequiredScheduleTagId", "StageConfigId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 1, 2 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 11, 2 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 8, 2 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 12, 2 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 7, 2 },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 13, 2 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 6, 2 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 14, 2 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 5, 2 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 15, 2 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 4, 2 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 3, 2 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 2, 2 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 9, 2 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "LiteStageFileConfigs",
                columns: new[] { "Id", "CreateDate", "IsLocation", "ModifiedByUser", "RequiredFileTagId", "StageConfigId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 761, 2 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 762, 2 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 765, 2 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 744, 1 }
                });

            migrationBuilder.InsertData(
                table: "StageConfig_RequiredSchedules",
                columns: new[] { "Id", "CreateDate", "ModifiedByUser", "RequiredScheduleTagId", "StageConfigId" },
                values: new object[,]
                {
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 10, 3 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 9, 3 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 2, 3 },
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 1, 3 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 15, 3 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 3, 3 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 14, 3 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 4, 3 },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 13, 3 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 6, 3 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 12, 3 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 7, 3 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 11, 3 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 8, 3 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "StageFileConfigs",
                columns: new[] { "Id", "CreateDate", "IsLocation", "ModifiedByUser", "RequiredFileTagId", "StageConfigId" },
                values: new object[,]
                {
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 758, 3 },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 759, 3 },
                    { 25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 759, 4 },
                    { 21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 760, 3 },
                    { 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 764, 4 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 761, 3 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 762, 3 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 763, 3 },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 757, 3 },
                    { 26, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 760, 4 },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 756, 3 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 750, 3 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 754, 3 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 753, 3 },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 752, 3 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 751, 3 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 749, 3 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 748, 3 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 747, 3 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 746, 3 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 745, 2 },
                    { 24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 744, 4 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 744, 3 },
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 744, 2 },
                    { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 766, 4 },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 755, 3 },
                    { 27, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, 777, 1 }
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
                name: "IX_FinancialData_BusinessCaseId",
                table: "FinancialData",
                column: "BusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_GateComments_GateId",
                table: "GateComments",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_GateFiles_GateId",
                table: "GateFiles",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperConfigs_StageConfigId",
                table: "GateKeeperConfigs",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperLites_GateId",
                table: "GateKeeperLites",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperLites_GateKeeperConfigId",
                table: "GateKeeperLites",
                column: "GateKeeperConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeeperLites_UserId",
                table: "GateKeeperLites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_GateId",
                table: "GateKeepers",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_GateKeeperConfigId",
                table: "GateKeepers",
                column: "GateKeeperConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_LiteGateKeeperConfigId",
                table: "GateKeepers",
                column: "LiteGateKeeperConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_UserId",
                table: "GateKeepers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gates_ProjectId",
                table: "Gates",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Gates_StageConfigId",
                table: "Gates",
                column: "StageConfigId");

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
                name: "IX_LiteGateKeeperConfigs_StageConfigId",
                table: "LiteGateKeeperConfigs",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteRequiredSchedules_RequiredScheduleTagId",
                table: "LiteRequiredSchedules",
                column: "RequiredScheduleTagId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteRequiredSchedules_StageConfigId",
                table: "LiteRequiredSchedules",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteStageFileConfigs_RequiredFileTagId",
                table: "LiteStageFileConfigs",
                column: "RequiredFileTagId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteStageFileConfigs_StageConfigId",
                table: "LiteStageFileConfigs",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Mitigations_ResponsibilityUserId",
                table: "Mitigations",
                column: "ResponsibilityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mitigations_RiskId",
                table: "Mitigations",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionalFiles_FileTagId",
                table: "OptionalFiles",
                column: "FileTagId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionalFiles_StageId",
                table: "OptionalFiles",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLaunchReviews_StageId",
                table: "PostLaunchReviews",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfrigmentPatentabilities_StageId",
                table: "ProductInfrigmentPatentabilities",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIntroChecklists_ApprovedByUserId",
                table: "ProductIntroChecklists",
                column: "ApprovedByUserId");

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
                name: "IX_ProjectJustifications_AddToInhouseTechnicalAbilitiesTagId",
                table: "ProjectJustifications",
                column: "AddToInhouseTechnicalAbilitiesTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJustifications_StageId",
                table: "ProjectJustifications",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStateHistories_ProjectId",
                table: "ProjectStateHistories",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ReportProjectId",
                table: "Report_BusinessCase",
                column: "ReportProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ManufacturingLocations_ManufacturingLocationsTagId",
                table: "Report_BusinessCase_ManufacturingLocations",
                column: "ManufacturingLocationsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_BusinessCase_ManufacturingLocations_ReportBusinessCaseId",
                table: "Report_BusinessCase_ManufacturingLocations",
                column: "ReportBusinessCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_FinancialData_ReportBusinessCaseId",
                table: "Report_FinancialData",
                column: "ReportBusinessCaseId");

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
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectCustomers_CustomersTagId",
                table: "Report_ProjectCustomers",
                column: "CustomersTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectCustomers_ReportProjectId",
                table: "Report_ProjectCustomers",
                column: "ReportProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectEndUserCountries_EndUserCountriesTagId",
                table: "Report_ProjectEndUserCountries",
                column: "EndUserCountriesTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectEndUserCountries_ReportProjectId",
                table: "Report_ProjectEndUserCountries",
                column: "ReportProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectSalesRegion_ReportProjectId",
                table: "Report_ProjectSalesRegion",
                column: "ReportProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectSalesRegion_SalesRegionTagId",
                table: "Report_ProjectSalesRegion",
                column: "SalesRegionTagId");

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
                name: "IX_Schedules_StageId",
                table: "Schedules",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TagId",
                table: "Schedules",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredSchedules_RequiredScheduleTagId",
                table: "StageConfig_RequiredSchedules",
                column: "RequiredScheduleTagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageConfig_RequiredSchedules_StageConfigId",
                table: "StageConfig_RequiredSchedules",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_StageFileConfigs_RequiredFileTagId",
                table: "StageFileConfigs",
                column: "RequiredFileTagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageFileConfigs_StageConfigId",
                table: "StageFileConfigs",
                column: "StageConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_StageFiles_FileTagId",
                table: "StageFiles",
                column: "FileTagId");

            migrationBuilder.CreateIndex(
                name: "IX_StageFiles_StageId",
                table: "StageFiles",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_ProjectId",
                table: "Stages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagCategoryId",
                table: "Tags",
                column: "TagCategoryId");

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
                name: "CustomerDesignApprovals");

            migrationBuilder.DropTable(
                name: "FinancialData");

            migrationBuilder.DropTable(
                name: "GateComments");

            migrationBuilder.DropTable(
                name: "GateFiles");

            migrationBuilder.DropTable(
                name: "GateKeeperLites");

            migrationBuilder.DropTable(
                name: "GateKeepers");

            migrationBuilder.DropTable(
                name: "InvestmentPlans");

            migrationBuilder.DropTable(
                name: "KeyCharacteristics");

            migrationBuilder.DropTable(
                name: "LiteRequiredSchedules");

            migrationBuilder.DropTable(
                name: "LiteStageFileConfigs");

            migrationBuilder.DropTable(
                name: "Mitigations");

            migrationBuilder.DropTable(
                name: "OptionalFiles");

            migrationBuilder.DropTable(
                name: "PostLaunchReviews");

            migrationBuilder.DropTable(
                name: "ProductInfrigmentPatentabilities");

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
                name: "ProjectStateHistories");

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
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "StageConfig_RequiredSchedules");

            migrationBuilder.DropTable(
                name: "StageFileConfigs");

            migrationBuilder.DropTable(
                name: "StageFiles");

            migrationBuilder.DropTable(
                name: "BusinessCases");

            migrationBuilder.DropTable(
                name: "Gates");

            migrationBuilder.DropTable(
                name: "GateKeeperConfigs");

            migrationBuilder.DropTable(
                name: "LiteGateKeeperConfigs");

            migrationBuilder.DropTable(
                name: "Risks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProjectDetails");

            migrationBuilder.DropTable(
                name: "Report_BusinessCase");

            migrationBuilder.DropTable(
                name: "StageConfigs");

            migrationBuilder.DropTable(
                name: "LiteStageConfigs");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Report_Project");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TagCategories");
        }
    }
}
