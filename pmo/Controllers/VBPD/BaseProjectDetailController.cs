namespace pmo.Controllers {
    using AutoMapper;
    using dbModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;
    using ViewModels.Helpers;

    public class BaseProjectDetailController : BaseController {
        private readonly ProjectDetailNav _nav;
        private readonly List<ActiveNav> _activeStageNavs;
        private readonly List<ActiveNav> _activeGateNavs;
        private readonly bool _displayGateDecisions;
        protected readonly int _projectId;
        protected readonly int _projectState;

        public BaseProjectDetailController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            // set nav component for all of these pages here
            this._projectId = int.Parse(Helpers.GetRouteValue(httpContextAccessor.HttpContext.Request, "projectId"));

            var activeStage = _context.Stages.IncludeAll()
               .Where(
                   w =>
                       w.ProjectId == _projectId &&
                       w.IsComplete == false
               ).OrderByDescending(
                   o =>
                       o.CreateDate
               ).FirstOrDefault();

            var activeGate = _context.Gates.IncludeAll()
                .Where(w =>
                    w.ProjectId == _projectId &&
                    (w.Decision == GateDecisionType.PendingDecision || w.Decision == GateDecisionType.Open)
                ).OrderByDescending(
                    o =>
                        o.CreateDate
                ).FirstOrDefault();

            this._displayGateDecisions = activeGate?.Decision == GateDecisionType.PendingDecision;
            this._activeStageNavs = activeStage == null ? new List<ActiveNav>() : GetActiveStageNavs(activeStage);
            this._activeGateNavs = activeGate == null ? new List<ActiveNav>() : GetActiveGateNavs(activeGate);
            this._projectState =(int)_context.ProjectStateHistories.Where(p => p.ProjectId == _projectId).OrderByDescending(d => d.CreateDate).First().ProjectState;
            this._nav = new ProjectDetailNav(_context, _mapper, _projectId);
        }

        private List<ActiveNav> GetActiveStageNavs(Stage activeStage) {
            var activeStageConfig = _context.StageConfigs.First(
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

            var commonPath = $"/vbpd-projects/{this._projectId}/stages/{activeStage.StageNumber}";

            var vals = new List<ActiveNav>() {
                    new ActiveNav {
                        Component = "Technical Feasibility & Scope",
                        IsComplete = activeStageConfig.MinProjectJustifications <= 0 || (activeStage.ProjectJustificationHistory.Count > 0 && activeStage.ProjectJustificationHistory.Max(m => m.Version) >= activeStageConfig.MinProjectJustifications),
                        Url = $"{commonPath}/project-justification/edit",
                        Version = activeStage.ProjectJustificationHistory.Count == 0 ? 0 : activeStage.ProjectJustificationHistory.Max(m => m.Version),
                    },
                    new ActiveNav {
                        Component = "Business Cases",
                        IsComplete = activeStageConfig.MinBusinessCases <= 0 || (activeStage.BusinessCaseHistory.Count > 0 && activeStage.BusinessCaseHistory.Max(m => m.Version) >= activeStageConfig.MinBusinessCases),
                        Url = $"{commonPath}/business-case/edit",
                        Version = activeStage.BusinessCaseHistory.Count == 0 ? 0 : activeStage.BusinessCaseHistory.Max(m => m.Version),
                    },
                    new ActiveNav {
                        Component = "Product Infrigment Patentabilities",
                        IsComplete = activeStageConfig.MinProductInfringementPatentabilities <= 0 || (activeStage.ProductInfrigmentPatentabilityHistory.Count > 0 && activeStage.ProductInfrigmentPatentabilityHistory.Max(m => m.Version) >= activeStageConfig.MinProductInfringementPatentabilities),
                        Url = $"{commonPath}/product-infrigment-patentability/edit",
                        Version = activeStage.ProductInfrigmentPatentabilityHistory.Count == 0 ? 0 : activeStage.ProductInfrigmentPatentabilityHistory.Max(m => m.Version),
                    },
                    new ActiveNav {
                        Component = "Key Characteristics",
                        IsComplete = activeStageConfig.MinKeyCharacteristics <= 0 || (activeStage.KeyCharacteristicHistory.Count > 0 && activeStage.KeyCharacteristicHistory.Max(m => m.Version) >= activeStageConfig.MinKeyCharacteristics),
                        Url = $"{commonPath}/key-characteristic/edit",
                        Version = activeStage.KeyCharacteristicHistory.Count == 0 ? 0 : activeStage.KeyCharacteristicHistory.Max(m => m.Version),
                    },
                    new ActiveNav {
                        Component = "Customer Design Approvals",
                        IsComplete = activeStageConfig.MinCustomerDesignApprovals <= 0 || (activeStage.CustomerDesignApprovalHistory.Count > 0 && activeStage.CustomerDesignApprovalHistory.Max(m => m.Version) >= activeStageConfig.MinCustomerDesignApprovals),
                        Url = $"{commonPath}/customer-design-approval/edit",
                        Version = activeStage.CustomerDesignApprovalHistory.Count == 0 ? 0 : activeStage.CustomerDesignApprovalHistory.Max(m => m.Version),
                    },
                    new ActiveNav {
                        Component = "Investment Plans",
                        IsComplete = activeStageConfig.MinInvestmentPlans <= 0 || (activeStage.InvestmentPlanHistory.Count > 0 && activeStage.InvestmentPlanHistory.Max(m => m.Version) >= activeStageConfig.MinInvestmentPlans),
                        Url = $"{commonPath}/investment-plan/edit",
                        Version = activeStage.InvestmentPlanHistory.Count == 0 ? 0 : activeStage.InvestmentPlanHistory.Max(m => m.Version),
                    },
                    new ActiveNav {
                        Component = "Product Intro Checklist",
                        IsComplete = activeStageConfig.MinProductIntroChecklist <= 0 || (activeStage.ProductIntroChecklistHistory.Count > 0 && activeStage.ProductIntroChecklistHistory.Max(m => m.Version) >= activeStageConfig.MinProductIntroChecklist),
                        Url = $"{commonPath}/product-intro-checklist/edit",
                        Version = activeStage.ProductIntroChecklistHistory.Count == 0 ? 0 : activeStage.ProductIntroChecklistHistory.Max(m => m.Version),
                    },
                };



            // only display schedules if configs permit
            if (countScheduleConfigs > 0) {
                vals.Add(
                    new ActiveNav {
                        Component = "Schedules",
                        IsComplete = activeStage.Schedules.Count == countScheduleConfigs,
                        Url = $"{commonPath}/schedules/edit",
                    });
            }

            // only display if files are meant for this stage
            if (countStageFileConfigs > 0) {
                vals.Add(
                    new ActiveNav {
                        Component = "Files",
                        IsComplete = activeStage.Files.Count == countStageFileConfigs,
                        Url = $"{commonPath}/files/edit",
                    });
            }

            // only allow risk analysis insert if configs say so
            if (activeStageConfig.AllowInsertRiskAssesments) {
                vals.Add(
                    new ActiveNav {
                        Component = "Risks",
                        IsComplete = true,
                        Url = $"{commonPath}/risk"
                    });
            }

            return vals;
        }

        private List<ActiveNav> GetActiveGateNavs (Gate activeGate) {
            // single edit page for time being
            return new List<ActiveNav>() {
                new ActiveNav {
                    IsComplete = activeGate.Decision == GateDecisionType.PendingDecision,
                    Component = "Edit Gate",
                    Url = $"/vbpd-projects/{this._projectId}/gates/edit",
                }
            };
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            ViewData["ProjectNav"] = _nav;
            ViewData["ActiveStageNavs"] = _activeStageNavs;
            bool showMoveToGateBtn = _activeStageNavs
                .FirstOrDefault(f => !f.IsComplete) == null &&
                _activeStageNavs.Count > 0;
            ViewData["DisplayMoveToGate"] = showMoveToGateBtn;
            ViewData["ActiveGateNavs"] = _activeGateNavs;
            ViewData["DisplayGateDecisions"] = this._displayGateDecisions;
            ViewData["ProjectState"] = _projectState;
            base.OnActionExecuting(filterContext);
        }
    }
}