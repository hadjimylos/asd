using System.Collections.Generic;

namespace dbModels {
    public class StageConfig: DatabaseModel {
        public int StageNumber { get; set; }

        public bool AllowInsertRiskAssesments { get; set; }

        public int MinProjectJustifications { get; set; }

        public int MinBusinessCases { get; set; }

        public int MinProductInfringementPatentabilities { get; set; }

        public int MinKeyCharacteristics { get; set; }

        public int MinCustomerDesignApprovals { get; set; }

        public int MinInvestmentPlans { get; set; }

        public int MinProductIntroChecklist { get; set; }

        public int MinRampResourcePlans { get; set; }

        public int MinQualificationTesting { get; set; }

        public int MinDesignConcepts { get; set; }

        public virtual List<StageConfig_RequiredSchedules> RequiredSchedules { get; set; }
    }
}
