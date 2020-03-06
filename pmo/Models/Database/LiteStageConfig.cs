namespace dbModels {
    using System.Collections.Generic;

    public class LiteStageConfig : DatabaseModel {
        public int StageNumber { get; set; }

        public bool AllowInsertRiskAssesments { get; set; }

        public int MinProjectJustifications { get; set; }

        public int MinBusinessCases { get; set; }

        public int MinProductInfringementPatentabilities { get; set; }

        public int MinKeyCharacteristics { get; set; }

        public int MinCustomerDesignApprovals { get; set; }

        public int MinInvestmentPlans { get; set; }

        public int MinProductIntroChecklist { get; set; }

        public int MinPostLaunchReviews { get; set; }

        public virtual List<LiteRequiredSchedule> RequiredSchedules { get; set; }
    }
}
