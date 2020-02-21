using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class InvestmentPlan: StageHistoryModel {
        public string ItemNumber { get; set; }

        public string Item { get; set; }

        public string PurchasedFrom { get; set; }

        public string ShipToLocation { get; set; }

        public string Cost { get; set; }

        public string Terms { get; set; }
    }
}