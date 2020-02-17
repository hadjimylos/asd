namespace ViewModels {
    using AutoMapper;
    using dbModels;
    using pmo;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Helpers;

    public class ProjectDetailNav {
        private readonly EfContext _context;
        private readonly IMapper _mapper;

        public ProjectDetailNav(EfContext context, IMapper mapper, int projectId) {
            _context = context;
            _mapper = mapper;
            this.ProjectId = projectId;
            populateNav(projectId);
        }

        public int ProjectId { get; set; }
        public List<NavigationStage> Stages { get; set; }
        public List<NavigationGate> Gates { get; set; }
        private void populateNav(int projectId) {
            var projectDetail = _context.ProjectDetails
                .Where(w => w.ProjectId == projectId)
                .OrderByDescending(o => o.CreateDate)
                .First();

            // format for navigation purposes
            var stages = _context.Stages
                .IncludeAll()
                .Where(w => w.ProjectId == projectId)
                .ToList();

            foreach (var stage in stages) {
                stage.ProjectJustificationHistory = stage.ProjectJustificationHistory.RemoveTransactions();
                stage.BusinessCaseHistory = stage.BusinessCaseHistory.RemoveTransactions();
                stage.ProductInfrigmentPatentabilityHistory = stage.ProductInfrigmentPatentabilityHistory.RemoveTransactions();
                stage.KeyCharacteristicHistory = stage.KeyCharacteristicHistory.RemoveTransactions();
                stage.CustomerDesignApprovalHistory = stage.CustomerDesignApprovalHistory.RemoveTransactions();
                stage.InvestmentPlanHistory = stage.InvestmentPlanHistory.RemoveTransactions();
                stage.ProductIntroChecklistHistory = stage.ProductIntroChecklistHistory.RemoveTransactions();
            }

            // convert to nav objects
            var stageNavs = _mapper.Map<List<NavigationStage>>(stages);
            stageNavs.ForEach(stage => {
                // map history components to nav
                stage.ProjectJustificationNavs = _mapper.Map<List<ProjectJustificationNav>>(stage.ProjectJustificationHistory);
                stage.BusinessCaseNavs = _mapper.Map<List<BusinessCaseNav>>(stage.BusinessCaseHistory);
                stage.ProductInfrigmentPatentabilityNavs = _mapper.Map<List<ProductInfrigmentPatentabilityNav>>(stage.ProductInfrigmentPatentabilityHistory);
                stage.KeyCharacteristicNavs = _mapper.Map<List<KeyCharacteristicNav>>(stage.KeyCharacteristicHistory);
                stage.CustomerDesignApprovalNavs = _mapper.Map<List<CustomerDesignApprovalNav>>(stage.CustomerDesignApprovalHistory);
                stage.InvestmentPlanNavs = _mapper.Map<List<InvestmentPlanNav>>(stage.InvestmentPlanHistory);
                stage.ProductIntroChecklistNavs = _mapper.Map<List<ProductIntroChecklistNav>>(stage.ProductIntroChecklistHistory);

                // set URLs for each nav component list
                stage.ProjectJustificationNavs.ForEach(item => item.Url = $"/vbpd-projects/{projectId}/stages/{stage.StageNumber}/project-justification/{item.Version}");
                stage.BusinessCaseNavs.ForEach(item => item.Url = $"/vbpd-projects/{projectId}/stages/{stage.StageNumber}/business-case/{item.Version}");
                stage.ProductInfrigmentPatentabilityNavs.ForEach(item => item.Url = $"/vbpd-projects/{projectId}/stages/{stage.StageNumber}/product-infrigment-patentability/{item.Version}");
                stage.KeyCharacteristicNavs.ForEach(item => item.Url = $"/vbpd-projects/{projectId}/stages/{stage.StageNumber}/key-characteristic/{item.Version}");
                stage.CustomerDesignApprovalNavs.ForEach(item => item.Url = $"/vbpd-projects/{projectId}/stages/{stage.StageNumber}/customer-design-approval/{item.Version}");
                stage.InvestmentPlanNavs.ForEach(item => item.Url = $"/vbpd-projects/{projectId}/stages/{stage.StageNumber}/investment-plan/{item.Version}");
                stage.ProductIntroChecklistNavs.ForEach(item => item.Url = $"/vbpd-projects/{projectId}/stages/{stage.StageNumber}/product-intro-checklist/{item.Version}");

                // add flat navigations
                stage.SchedulesUrl = $"/vbpd-projects/{projectId}/stages/{stage.StageNumber}/schedules/detail";
                stage.DisplaySchedules = stage.Schedules.Count > 0;
                stage.RisksUrl = $"/vbpd-projects/{projectId}/stages/{stage.StageNumber}/risks";
                stage.DisplayRisks = stage.Risks.Count > 0;
            });

            var gates = _context.Gates
                .Where(w => w.ProjectId == projectId)
                .ToList();

            var gateNavs = _mapper.Map<List<NavigationGate>>(gates);

            this.Gates = gateNavs;
            this.Stages = stageNavs;
        }

    }

    public class NavigationStage : Stage {
        public List<ProjectJustificationNav> ProjectJustificationNavs { get; set; }
        public List<BusinessCaseNav> BusinessCaseNavs { get; set; }
        public List<ProductInfrigmentPatentabilityNav> ProductInfrigmentPatentabilityNavs { get; set; }
        public List<KeyCharacteristicNav> KeyCharacteristicNavs { get; set; }
        public List<CustomerDesignApprovalNav> CustomerDesignApprovalNavs { get; set; }
        public List<InvestmentPlanNav> InvestmentPlanNavs { get; set; }
        public List<ProductIntroChecklistNav> ProductIntroChecklistNavs { get; set; }
        public string SchedulesUrl { get; set; }
        public bool DisplaySchedules { get; set; }
        public string RisksUrl { get; set; }
        public bool DisplayRisks { get; set; }
}

    public class NavigationGate : Gate {
        public string Url { get; set; }
    }
}
