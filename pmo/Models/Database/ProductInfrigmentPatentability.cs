namespace dbModels {
    using System;

    public class ProductInfrigmentPatentability : StageHistoryModel {
        public string PatentNumber { get; set; }

        public string Issue { get; set; }

        public string MitigationStrategy { get; set; }

        public bool ContainsFeaturesRequiringIPProtection { get; set; }

        public bool InventionDisclosureSubmitted { get; set; }

        public DateTime ProductFirstTimeOfferedForSale { get; set; }
    }
}