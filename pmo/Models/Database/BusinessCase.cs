using System;
using System.Collections.Generic;
using System.Linq;

namespace dbModels {
    public class BusinessCase: StageHistoryModel {

        public bool WillCustomerFundQual { get; set; }

        public bool WillCustomerFundTooling { get; set; }

        public decimal ProbabiltyOfWin { get; set; }

        public decimal TargetFirstYearGrossMargin { get; set; }

        public int FinancialStartYear { get; set; }

        public decimal DiscountRate { get; set; }

        public decimal TaxRate { get; set; }

        public decimal LaborRate { get; set; }

        public string GpaRequirements { get; set; }

        public decimal MultipleFieldsGeneratedTable { get; set; }

        public string ProjectScope { get; set; }

        public decimal WorkRequirementAmount { get; set; }

        public bool StrategicAlignment { get; set; }

        public string AddToTecnicalAbilities { get; set; }

        public DateTime ProjectCompletion { get; set; }

        public decimal TimeFromProjectCompletionToRevenueGeneration { get; set; }

        public string CustomerMarketAnalysis { get; set; }

        public bool Changes { get; set; }

        public virtual List<BusinessCase_ManufacturingLocation> ManufacturingLocations { get; set; }
        public virtual List<FinancialData> FinancialData { get; set; }

        public decimal GetNPV() =>
            FinancialData.Sum(s => s.GetPresentValue());

        public decimal GetROI() =>
            (
                FinancialData.Sum(s => s.GetStandardMarginPrice()) *
                (1 -
                    (this.TaxRate / 100)
                ) /
                FinancialData.Count
            ) /
            FinancialData.Sum(s => s.GetTotalExpenses());

        public decimal GetPaybackPeriod() {
            var firstPositive = FinancialData.First(f => f.GetCumulativeCashFlow() > 0);

            return 
                (
                    firstPositive.Year -
                    this.FinancialStartYear
                ) +
                (
                    1 -
                    firstPositive.GetCumulativeCashFlow() /
                    firstPositive.GetFreeCashFlow()
                );
        }
    }
}