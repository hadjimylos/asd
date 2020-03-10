namespace dto {
    public class FinancialDataforPPT {
        public  int Year { get; set; }

        public  int Quantity { get; set; }

        public  decimal StdCostEstimated { get; set; }

        public  decimal SalesPriceEstimated { get; set; }

        public decimal CostExtended { get; set; }
          

        public decimal RevenueExtended { get; set; }
         

        public decimal StandardMarginPrice { get; set; }
          

        public decimal StandardMarginPercent { get; set; }

        public  decimal GPACapital { get; set; }
        public  decimal GPAExpense { get; set; }
        public  decimal QualCosts { get; set; }
        public  decimal OtherDevelopmentExpenses { get; set; }

        public decimal TotalExpenses { get; set; }

    }
}