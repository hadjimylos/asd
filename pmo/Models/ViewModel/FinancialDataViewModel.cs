using dbModels;

namespace ViewModels
{
    public class FinancialDataViewModel : FinancialData
    {  
        public bool Totals { get; set; }

        //public decimal CostExtended { get { return Quantity * StdCostEstimated; } set { } }
        //public decimal RevenueExtended { get { return Quantity * SalesPriceEstimated; } }
        //public decimal StdMarginEstimatedDollar { get { return RevenueExtended - CostExtended; } }
        //public decimal StdMarginEstimatedPercent { get { return StdMarginEstimatedDollar / RevenueExtended; } }
        //public decimal TotalExpenses { get { return GPACapital + GPAExpense + QualCosts + OtherDevelopmentExpenses; } }
        //public decimal NetProfitBeforeTax { get { return RevenueExtended - CostExtended - QualCosts - OtherDevelopmentExpenses; } }
        //public decimal NetProfitAfterTax { get { return NetProfitBeforeTax * (1 - (BusinessCase.TaxRate / 100)); } }
        //public decimal FreeCashFlow { get { return NetProfitAfterTax - GPACapital; } }
        //public decimal PresentValue { get { return FreeCashFlow / Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + (BusinessCase.DiscountRate / 100)), Convert.ToDouble(Index))); } }

        public decimal CostExtended { get; set; }
        public decimal RevenueExtended { get; set; }
        public decimal StdMarginEstimatedDollar { get; set; }
        public decimal StdMarginEstimatedPercent { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfitBeforeTax { get; set; }
        public decimal NetProfitAfterTax { get; set; }
        public decimal FreeCashFlow { get; set; }
        public decimal PresentValue { get; set; }
        public decimal CumulativeCashFlow { get; set; }



    }
}
