using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace dbModels {
    public class FinancialData : DatabaseModel {
        public int BusinessCaseId { get; set; }

        [ForeignKey("BusinessCaseId")]
        public virtual BusinessCase BusinessCase { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
        public decimal StdCostEstimated { get; set; }
        public decimal SalesPriceEstimated { get; set; }
        public decimal? GPACapital { get; set; }
        public decimal? GPAExpense { get; set; }
        public decimal? QualCosts { get; set; }
        public decimal? OtherDevelopmentExpenses { get; set; }

        public decimal GetCostExtended() =>
            this.Quantity *
            this.StdCostEstimated;

        public decimal GetRevenueExtended() =>
            this.Quantity *
            this.SalesPriceEstimated;

        public decimal GetStandardMarginPrice() =>
            this.GetRevenueExtended() -
            this.GetCostExtended();

        public decimal GetStandardMarginPercent() =>
            this.GetStandardMarginPrice() /
            this.GetRevenueExtended();

        public decimal GetTotalExpenses() =>
            this.GPACapital ?? 0 +
            this.GPAExpense ?? 0 +
            this.QualCosts ?? 0 +
            this.OtherDevelopmentExpenses ?? 0;

        public decimal GetNetProfitBeforeTax() =>
            this.GetRevenueExtended() -
            this.GetCostExtended() -
            this.QualCosts ?? 0 -
            this.OtherDevelopmentExpenses ?? 0;

        public decimal GetNetProfitAfterTax() =>
            this.GetNetProfitBeforeTax() *
                (1 -
                    (this.BusinessCase.TaxRate / 100)
            );

        public decimal GetFreeCashFlow() =>
            this.GetNetProfitAfterTax() -
            this.GPACapital ?? 0;

        public decimal GetPresentValue() =>
            this.GetFreeCashFlow() /
                Convert.ToDecimal
                (
                    Math.Pow((double)
                    (1 +
                        (this.BusinessCase.DiscountRate / 100)
                    ),
                        (this.Year - this.BusinessCase.FinancialStartYear)
                    )
                );

        public decimal GetCumulativeCashFlow() =>
            this.Year == this.BusinessCase.FinancialStartYear ?
                this.GetFreeCashFlow() :
                    this.BusinessCase.FinancialData.First(f => f.Year == this.Year - 1).GetCumulativeCashFlow() +
                    this.GetFreeCashFlow();
    }
}
