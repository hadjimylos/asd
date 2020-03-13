using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public class Stage : DatabaseModel
    {
        public int ProjectId { get; set; }

        [Required]
        public bool IsComplete { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public int StageNumber { get; set; }
        public virtual List<Schedule> Schedules { get; set; }
        public virtual List<Risk> Risks { get; set; }

        public virtual List<ProjectJustification> ProjectJustificationHistory { get; set; }
        public virtual List<BusinessCase> BusinessCaseHistory { get; set; }
        public virtual List<ProductInfrigmentPatentability> ProductInfrigmentPatentabilityHistory { get; set; }
        public virtual List<KeyCharacteristic> KeyCharacteristicHistory { get; set; }
        public virtual List<CustomerDesignApproval> CustomerDesignApprovalHistory { get; set; }
        public virtual List<InvestmentPlan> InvestmentPlanHistory { get; set; }
        public virtual List<ProductIntroChecklist> ProductIntroChecklistHistory { get; set; }
        public virtual List<PostLaunchReview> PostLaunchReviewHistory { get; set; }
        public virtual List<StageFile> Files { get; set; }
        public virtual List<OptionalFile> OptionalFiles { get; set; }
    }
}