namespace pmo.Controllers {
    using AutoMapper;
    using dbModels;
    using dto;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Helpers;

    public class BaseProjectDetailController : BaseController {
        private readonly ProjectDetailNav _nav;
        private readonly List<ActiveNav> _activeStageNavs;
        private readonly List<ActiveNav> _activeGateNavs;
        private readonly bool _displayGateDecisions;
        private readonly bool _isFinalGate;
        private readonly string _displayNum;
        protected readonly int _projectId;
        protected readonly int _projectState;
        protected readonly bool _isLite;
        
        

        public BaseProjectDetailController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            this._projectId = int.Parse(Helpers.GetRouteValue(httpContextAccessor.HttpContext.Request, "projectId"));
            this._projectState = (int)_context.ProjectStateHistories.Where(p => p.ProjectId == _projectId).OrderByDescending(d => d.CreateDate).First().ProjectState;
            // set nav component for all of these pages here
            this._isLite = _context.ProjectDetails
                .Include(i => i.ProjectCategory)
                .Where(w => w.ProjectId == _projectId)
                .RemoveTransactions()
                .OrderByDescending(c => c.CreateDate)
                .First()
                .ProjectCategory.Id == 219; // minor modification
            this._nav = new ProjectDetailNav(_context, _mapper, _projectId);

            // don't run code if project is complete to determine active navs
            if (_projectState == (int)ProjectState.Complete)
                return;

            var activeStage = _context.Stages.IncludeAll()
               .Where(
                   w =>
                       w.ProjectId == _projectId &&
                       w.IsComplete == false
               ).OrderByDescending(
                   o =>
                       o.CreateDate
               ).FirstOrDefault();

            var gate =
                activeStage != null ? // only run query if necessary
                null :
                _context.Gates.IncludeAll()
                .Where(w =>
                    w.ProjectId == _projectId
                ).OrderByDescending(
                    o =>
                        o.CreateDate
                ).FirstOrDefault();

            this._displayGateDecisions = gate?.Decision == GateDecisionType.PendingDecision || gate?.Decision == GateDecisionType.OnHold || gate?.Decision == GateDecisionType.Closed;
            this._activeStageNavs = activeStage == null ? new List<ActiveNav>() : _isLite ? GetLiteActiveStageNavs(activeStage) : GetActiveStageNavs(activeStage);
            this._activeGateNavs = gate == null ? new List<ActiveNav>() : GetActiveGateNavs(gate);
            this._isFinalGate = !_isLite?
                 gate?.StageConfig.StageNumber == _context.StageConfigs.Count() :
                 gate?.StageConfig.StageNumber == _context.LiteStageConfigs.Count();
            this._displayNum = !this._isLite ?
                activeStage?.StageNumber.ToString() ?? gate?.StageConfig.StageNumber.ToString() :
                activeStage != null ?
                    ((char)(activeStage.StageNumber + 64)).ToString() :
                    ((char)(gate.StageConfig.StageNumber + 64)).ToString();
                        
        }

        private List<ActiveNav> GetActiveStageNavs(Stage activeStage) {
            var activeStageConfig =
                _context.StageConfigs.First(
                    w =>
                        w.StageNumber == activeStage.StageNumber
                );

            var countScheduleConfigs = _context.StageConfig_RequiredSchedules
                .Where(
                    w =>
                        w.StageConfigId == activeStageConfig.Id
                ).Count();

            var countStageFileConfigs = _context.StageFileConfigs
                .Where(
                    w =>
                        w.StageConfigId == activeStageConfig.Id
                ).Select(s => s.RequiredFileTagId)
                .Distinct()
                .Count();

            return GetActiveNavs(activeStageConfig, activeStage, countScheduleConfigs, countStageFileConfigs);
        }

        private List<ActiveNav> GetLiteActiveStageNavs(Stage activeStage) {
            var activeStageConfig =
                _context.LiteStageConfigs.First(
                    w =>
                        w.StageNumber == activeStage.StageNumber
                );

            var countScheduleConfigs = _context.LiteRequiredSchedules
                .Where(
                    w =>
                        w.StageConfigId == activeStageConfig.Id
                ).Count();

            var countStageFileConfigs = _context.LiteStageFileConfigs
                .Where(
                    w =>
                        w.StageConfigId == activeStageConfig.Id
                ).Select(s => s.RequiredFileTagId)
                .Distinct()
                .Count();

            return GetActiveNavs(activeStageConfig, activeStage, countScheduleConfigs, countStageFileConfigs);
        }

        private List<ActiveNav> GetActiveNavs<T>(T activeStageConfig, Stage activeStage, int countScheduleConfigs, int countStageFileConfigs) where T : BaseStageConfig {
            var activeBusinessCase = activeStage
                .BusinessCaseHistory
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();

            var commonPath = $"/projects/{this._projectId}/stages/{activeStage.StageNumber}";
            return new List<ActiveNav>() {
                    new ActiveNav {
                        Component = "Technical Feasibility & Scope",
                        IsComplete = activeStageConfig.MinProjectJustifications <= 0 || (activeStage.ProjectJustificationHistory.Count > 0 && activeStage.ProjectJustificationHistory.Max(m => m.Version) >= activeStageConfig.MinProjectJustifications),
                        Url = $"{commonPath}/project-justification/edit",
                        Visible = activeStageConfig.MinProjectJustifications > 0,
                    },
                    new ActiveNav {
                        Component = "Business Cases",
                        IsComplete = activeStageConfig.MinBusinessCases <= 0 || (activeStage.BusinessCaseHistory.Count > 0 && activeStage.BusinessCaseHistory.Max(m => m.Version) >= activeStageConfig.MinBusinessCases),
                        Url = $"{commonPath}/business-case/edit",
                        Visible = activeStageConfig.MinBusinessCases > 0,

                        ChildNavs = new List<ActiveNav>(){
                            new ActiveNav {
                                Component = "Financials",
                                IsComplete = activeStageConfig.MinBusinessCases <= 0 || (activeBusinessCase?.FinancialData.Count > 0),
                                Url = $"{commonPath}/business-case/{activeBusinessCase?.Id}/financial-data/edit",
                                Visible = activeStageConfig.MinBusinessCases > 0 && activeBusinessCase != null,
                            }
                        }
                    },
                    new ActiveNav {
                        Component = "Product Infrigment Patentabilities",
                        IsComplete = activeStageConfig.MinProductInfringementPatentabilities <= 0 || (activeStage.ProductInfrigmentPatentabilityHistory.Count > 0 && activeStage.ProductInfrigmentPatentabilityHistory.Max(m => m.Version) >= activeStageConfig.MinProductInfringementPatentabilities),
                        Url = $"{commonPath}/product-infrigment-patentability/edit",
                        Visible = activeStageConfig.MinProductInfringementPatentabilities > 0,
                    },
                    new ActiveNav {
                        Component = "Key Characteristics",
                        IsComplete = activeStageConfig.MinKeyCharacteristics <= 0 || (activeStage.KeyCharacteristicHistory.Count > 0 && activeStage.KeyCharacteristicHistory.Max(m => m.Version) >= activeStageConfig.MinKeyCharacteristics),
                        Url = $"{commonPath}/key-characteristic/edit",
                        Visible = activeStageConfig.MinKeyCharacteristics > 0,
                    },
                    new ActiveNav {
                        Component = "Customer Design Approvals",
                        IsComplete = activeStageConfig.MinCustomerDesignApprovals <= 0 || (activeStage.CustomerDesignApprovalHistory.Count > 0 && activeStage.CustomerDesignApprovalHistory.Max(m => m.Version) >= activeStageConfig.MinCustomerDesignApprovals),
                        Url = $"{commonPath}/customer-design-approval/edit",
                        Visible = activeStageConfig.MinCustomerDesignApprovals > 0,
                    },
                    new ActiveNav {
                        Component = "Investment Plans",
                        IsComplete = activeStageConfig.MinInvestmentPlans <= 0 || (activeStage.InvestmentPlanHistory.Count > 0 && activeStage.InvestmentPlanHistory.Max(m => m.Version) >= activeStageConfig.MinInvestmentPlans),
                        Url = $"{commonPath}/investment-plan/edit",
                        Visible = activeStageConfig.MinInvestmentPlans > 0,
                    },
                    new ActiveNav {
                        Component = "Product Intro Checklist",
                        IsComplete = activeStageConfig.MinProductIntroChecklist <= 0 || (activeStage.ProductIntroChecklistHistory.Count > 0 && activeStage.ProductIntroChecklistHistory.Max(m => m.Version) >= activeStageConfig.MinProductIntroChecklist),
                        Url = $"{commonPath}/product-intro-checklist/edit",
                        Visible = activeStageConfig.MinProductIntroChecklist > 0,
                    },
                    new ActiveNav {
                        Component = "Post Launch Review",
                        IsComplete = activeStageConfig.MinPostLaunchReviews <= 0 || (activeStage.PostLaunchReviewHistory.Count > 0 && activeStage.PostLaunchReviewHistory.Max(m => m.Version) >= activeStageConfig.MinPostLaunchReviews),
                        Url = $"{commonPath}/post-launch-review/edit",
                        Visible = activeStageConfig.MinPostLaunchReviews > 0,
                    },
                    new ActiveNav {
                        Component = "Schedules",
                        IsComplete = activeStage.Schedules.Count >= countScheduleConfigs,
                        Url = $"{commonPath}/schedules/edit",
                        Visible = countScheduleConfigs > 0,
                    },
                    new ActiveNav {
                        Component = "Required Documents",
                        IsComplete = activeStage.Files.Select(s => s.FileTagId).Distinct().Count() == countStageFileConfigs,
                        Url = $"{commonPath}/files/edit",
                        Visible = countStageFileConfigs > 0,
                    },
                    new ActiveNav {
                        Component = "Risks",
                        IsComplete = true,
                        Url = $"{commonPath}/risk",
                        Visible = activeStageConfig.AllowInsertRiskAssesments,
                    },
                };
        }

        private List<ActiveNav> GetActiveGateNavs (Gate activeGate) {
            var stageNumber = activeGate.StageConfig.StageNumber;

            // single edit page for time being
            return new List<ActiveNav>() {
                new ActiveNav {
                    IsComplete = activeGate.Decision == GateDecisionType.PendingDecision,
                    Component = "Edit Gate",
                    Url = $"/projects/{this._projectId}/gates/edit",
                    Visible = true,
                },
                new ActiveNav {
                    Component = "Gate Files",
                    Url = $"/projects/{this._projectId}/stages/{stageNumber}/gate-files/edit",
                    Visible = true,
                }
            };
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            ViewData["ProjectNav"] = _nav;
            ViewData["ActiveStageNavs"] = _activeStageNavs;
            ViewData["ActiveGateNavs"] = _activeGateNavs;
            ViewData["DisplayGateDecisions"] = this._displayGateDecisions;
            ViewData["ProjectState"] = _projectState;
            ViewData["DisplayNumber"] = this._displayNum;
            ViewData["IsFinalGate"] = this._isFinalGate;
            base.OnActionExecuting(filterContext);
        }
    }
}