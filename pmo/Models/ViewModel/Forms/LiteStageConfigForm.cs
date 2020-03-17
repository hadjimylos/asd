using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class LiteStageConfigForm : LiteStageConfig {

        public LiteStageConfigForm() {
            ScheduleIds = new List<int>();
        }

        public bool isCreate { get; set; }

        public List<SelectListItem> SelectSchedules { get; set; }

        public List<int> ScheduleIds { get; set; }

        public List<string> GateKeepers { get; set; }

        //Temporary Checkboxes
        [Display(Name = "PROJECT JUSTIFICATION")]
        public bool MinProjectJustificationsCheck { get; set; }
        [Display(Name = "BUSINESS CASES")]
        public bool MinBusinessCasesCheck { get; set; }
        [Display(Name = "PRODUCT INFRINGEMENT PATENTABILITIES")]
        public bool MinProductInfringementPatentabilitiesCheck { get; set; }
        [Display(Name = "KEY CHARACTERISTICS")]
        public bool MinKeyCharacteristicsCheck { get; set; }
        [Display(Name = "CUSTOMER DESIGN APPROVALS")]
        public bool MinCustomerDesignApprovalsCheck { get; set; }
        [Display(Name = "INVESTMENT PLANS")]
        public bool MinInvestmentPlansCheck { get; set; }
        [Display(Name = "PRODUCT INTRO CHECKLIST")]
        public bool MinProductIntroChecklistCheck { get; set; }
        [Display(Name = "POST LAUNCH REVIEWS")]
        public bool MinPostLaunchReviewsCheck { get; set; }

    }
}