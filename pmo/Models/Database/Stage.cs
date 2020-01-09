using System.Collections.Generic;

namespace dbModels {
    public class Stage : HistoryModel {
        //TODO: Complete Properties
        public int StageNumber { get; set; }
        public virtual List<Schedule> ScheduleHistory { get; set; }
        public virtual List<DesignConcept> DesignConceptHistory { get; set; }
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