using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class Stage : HistoryModel {

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public int StageNumber { get; set; }
        public virtual List<Schedule> ScheduleHistory { get; set; }
        public virtual List<StageDesignConcept> StageDesignConceptHistory { get; set; }
        public virtual List<Risk> RiskHistory { get; set; }

        public virtual List<ProjectJustification> ProjectJustificationHistory { get; set; }
        public virtual List<BusinessCase> BusinessCaseHistory { get; set; }
        public virtual List<ProductInfrigmentPatentability> ProductInfrigmentPatentabilityHistory { get; set; }

        public virtual List<KeyCharacteristic> KeyCharacteristicHistory { get; set; }

       public virtual List<CustomerDesignApproval> CustomerDesignApprovalHistory { get; set; }
        public virtual List<InvestmentPlan> InvestmentPlanHistory { get; set; }
        public virtual List<ProductIntroChecklist> ProductIntroChecklistHistory { get; set; }
        public virtual List<RampResourcePlan> RampResourcePlanHistory { get; set; }

       public virtual List<QualificationTesting> QualificationTestingHistory { get; set; }
    }
}