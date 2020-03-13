using System.ComponentModel.DataAnnotations.Schema;


namespace dbModels.Report
{
    public class Report_FinancialData : Report_DatabaseModel
    {

        public int ReportBusinessCaseId { get; set; }
        [ForeignKey("ReportBusinessCaseId")]
        public virtual Report_BusinessCase Report_BusinessCase { get; set; }

        public int Year { get; set; }
        public int Quantity { get; set; }
        public decimal StdCostEstimated { get; set; }
        public decimal SalesPriceEstimated { get; set; }
        public decimal? GPACapital { get; set; }
        public decimal? GPAExpense { get; set; }
        public decimal? QualCosts { get; set; }
        public decimal? OtherDevelopmentExpenses { get; set; }

        public decimal GetCostExtended { set; get; }
        public decimal GetRevenueExtended { set; get; }
        public decimal GetStandardMarginPrice { set; get; }
        public decimal? GetStandardMarginPercent { set; get; }
        public decimal GetTotalExpenses { set; get; }
        public decimal GetNetProfitBeforeTax { set; get; }
        public decimal GetNetProfitAfterTax { set; get; }
        public decimal GetFreeCashFlow { set; get; }
        public decimal? GetPresentValue { set; get; }
        public decimal GetCumulativeCashFlow { set; get; }
    }
}