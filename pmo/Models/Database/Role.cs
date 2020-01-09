namespace dbModels {
    public class Role : DatabaseModel {
        public string FriendlyName { get; set; }

        public string Key { get; set; }

        public bool IsAdmin { get; set; }

        public bool CanInitiateProject { get; set; }

        public bool ManagesPauseProject { get; set; }

        public bool ManagesProjectDetail { get; set; }

        public bool ManagesProjectTeam { get; set; }

        public bool ManagesScheduling { get; set; }

        public bool ManagesBusinessCases { get; set; }

        public bool ManagesDeliverableRegisters { get; set; }

        public bool ManagesIntellectualProperty { get; set; }

        public bool ManagesProjectRequirements { get; set; }

        public bool ManagesRiskAnalysis { get; set; }

        public bool ManagesParts { get; set; }

        public bool ManagesCustomerDesignApproval { get; set; }

        public bool ManagesInvestmentPlan { get; set; }

        public bool ManagesMarketingPlan { get; set; }

        public bool ManagesRampAndResourcePlan { get; set; }

        public bool ManagesDeliverableRegister { get; set; }

        public bool ManagesQualificationTesting { get; set; }
    }
}
