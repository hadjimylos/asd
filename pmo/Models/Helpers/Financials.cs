using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace pmo.Models.Helpers
{
    public class Financials
    {
        #region Populate ViewModels
        public BusinessCaseViewModel GenerateFinancialDataRows(BusinessCaseViewModel model)
        {
            if (model.FinancialData.Count == 0)
            {
                for (int i = 0; i < model.NumberOfYears; i++)
                {
                    model.FinancialData.Add(new FinancialDataViewModel()
                    {
                        Totals = false,
                        Year = model.DataStartingDate.Year + i,
                        Quantity = 0,
                        StdCostEstimated = 0,
                        SalesCostEstimated = 0,
                        GPACapital = 0,
                        GPAExpense = 0,
                        QualCosts = 0,
                        OtherDevelopmentExpenses = 0
                    });
                }
                model.FinancialData.Add(new FinancialDataViewModel()
                {
                    Totals = true,
                    Year = 0,
                    Quantity = 0,
                    StdCostEstimated = 0,
                    SalesCostEstimated = 0,
                    GPACapital = 0,
                    GPAExpense = 0,
                    QualCosts = 0,
                    OtherDevelopmentExpenses = 0
                });

            }

            return model;
        }

        public BusinessCaseViewModel GenerateFinancialsCalculationsBusinessCase(BusinessCaseViewModel BusinessCase)
        {
            var Totals = BusinessCase.FinancialData.Where(x => x.Totals).First();
            BusinessCase.NPV = Totals.PresentValue;
            BusinessCase.ROI = ROI(Totals.StdCostEstimated,BusinessCase.TaxRate, BusinessCase.NumberOfYears, Totals.TotalExpenses);
            BusinessCase.PaybackPeriodYears = PaybackPeriodYears(BusinessCase.FinancialData.Where(x=>!x.Totals).ToList());
            return BusinessCase;
        }
        
        public List<FinancialDataViewModel> GenerateFinancialsCalculations(List<FinancialDataViewModel> FinancialData)
        {
            if (FinancialData.Where(x => !x.Totals).ToList().Count == FinancialData[0].BusinessCase.NumberOfYears)
            {
                var i = 0;
                foreach (var item in FinancialData)
                {
                    if (!item.Totals)
                    {
                        item.CostExtended = CostExtended(item.Quantity, item.StdCostEstimated);
                        item.RevenueExtended = RevenueExtended(item.QualCosts, item.SalesCostEstimated);
                        item.StdMarginEstimatedDollar = StdMarginEstimatedDollar(item.RevenueExtended, item.CostExtended);
                        item.StdMarginEstimatedPercent = StdMarginEstimatedPercent(item.StdMarginEstimatedDollar, item.RevenueExtended);
                        item.StdMarginEstimatedPercent = StdMarginEstimatedPercent(item.StdMarginEstimatedDollar, item.RevenueExtended);
                        item.TotalExpenses = TotalExpenses(item.GPACapital, item.GPAExpense, item.QualCosts, item.OtherDevelopmentExpenses);
                        item.NetProfitBeforeTax = NetProfitBeforeTax(item.RevenueExtended, item.CostExtended, item.QualCosts, item.OtherDevelopmentExpenses);
                        item.NetProfitAfterTax = NetProfitAfterTax(item.NetProfitBeforeTax, item.BusinessCase.TaxRate);
                        item.FreeCashFlow = FreeCashFlow(item.NetProfitAfterTax, item.GPACapital);
                        item.PresentValue = PresentValue(item.FreeCashFlow, item.BusinessCase.DiscountRate, item.BusinessCase.DataStartingDate.Year, item.Year);
                        if (i == 0)
                        {
                            item.CumulativeCashFlow = CumulativeCashFlow(0.00M, item.FreeCashFlow);
                        }
                        else
                        {
                            item.CumulativeCashFlow = CumulativeCashFlow(FinancialData[i].CumulativeCashFlow, item.FreeCashFlow);
                        }
                        i++;
                    }
                    else
                    {
                        item.CostExtended = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.CostExtended).ToList());
                        item.RevenueExtended = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.RevenueExtended).ToList());
                        item.StdMarginEstimatedDollar = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.StdMarginEstimatedDollar).ToList());
                        item.StdMarginEstimatedPercent = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.StdMarginEstimatedPercent).ToList());
                        item.StdMarginEstimatedPercent = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.StdMarginEstimatedPercent).ToList());
                        item.TotalExpenses = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.TotalExpenses).ToList());
                        item.NetProfitBeforeTax = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.NetProfitBeforeTax).ToList());
                        item.NetProfitAfterTax = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.NetProfitAfterTax).ToList());
                        item.FreeCashFlow = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.FreeCashFlow).ToList());
                        item.PresentValue = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.PresentValue).ToList());
                        item.CumulativeCashFlow = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.CumulativeCashFlow).ToList());
                        item.Quantity = Convert.ToInt32(TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.Quantity).ToList()));
                        item.StdCostEstimated = 0.00M;
                        item.SalesCostEstimated = 0.00M;

                    }

                }


            }

            return FinancialData;
        }
        #endregion

        #region Calculation Functions

        public decimal PaybackPeriodYears(List<FinancialDataViewModel> financialDataViewModel)
        {
            var i = 0;
            foreach (var item in financialDataViewModel)
            {
                if (item.CumulativeCashFlow>0)
                {
                    var CumulativeDivFreeCashFlow = Decimal.Divide(item.CumulativeCashFlow, item.FreeCashFlow);
                    return Math.Round(Decimal.Add(i, Decimal.Subtract(1, CumulativeDivFreeCashFlow)), 2);
                }
                i++;
            }
            return 0.00M;
        }

        public decimal ROI (decimal StdMarginEstimatedTotal, decimal TaxRate, int Years, decimal TotalExpences)
        {
            decimal BusinessTaxRatePercent = Decimal.Divide(TaxRate, 100);
            
            var Divide1 = Decimal.Multiply(StdMarginEstimatedTotal, (1- BusinessTaxRatePercent));
            try
            { 
                var Divide2 = Decimal.Divide(Divide1, Years);
                return Math.Round(Decimal.Divide(Divide2, TotalExpences), 2);
            }
            catch (DivideByZeroException ex)
            {
                return 0.00M;
            }
        }
        public decimal CostExtended(decimal Quantity, decimal StdCostEstimated)
        {
            return Math.Round(Decimal.Multiply(Quantity, StdCostEstimated), 2);
        }
        public decimal RevenueExtended(decimal Quantity, decimal SalesCostEstimated)
        {
            return Math.Round(Decimal.Multiply(Quantity, SalesCostEstimated), 2);
        }
        public decimal StdMarginEstimatedDollar(decimal RevenueExtended, decimal CostExtended)
        {
            return Math.Round(Decimal.Subtract(RevenueExtended, CostExtended), 2);
        }
        public decimal StdMarginEstimatedPercent(decimal StdMarginEstimatedDollar, decimal RevenueExtended)
        {
            try
            {
                return Math.Round(Decimal.Divide(StdMarginEstimatedDollar, RevenueExtended), 2);
            }
            catch (DivideByZeroException ex)
            {
                return 0.00M;
            }

        }
        public decimal TotalExpenses(decimal GPACapital, decimal GPAExpense, decimal QualCosts, decimal OtherDevelopmentExpenses)
        {
            return Math.Round(GPACapital + GPAExpense + QualCosts + OtherDevelopmentExpenses, 2);
        }
        public decimal NetProfitBeforeTax(decimal RevenueExtended, decimal CostExtended, decimal QualCosts, decimal OtherDevelopmentExpenses)
        {
            return Math.Round(RevenueExtended - CostExtended - QualCosts - OtherDevelopmentExpenses, 2);
        }
        public decimal NetProfitAfterTax(decimal NetProfitBeforeTax, decimal TaxRate)
        {
            decimal BusinessTaxRatePercent = Decimal.Divide(TaxRate, 100);
            return Math.Round(Decimal.Multiply(NetProfitBeforeTax, Decimal.Subtract(1, BusinessTaxRatePercent)), 2);
        }
        public decimal FreeCashFlow(decimal NetProfitAfterTax, decimal GPACapital)
        {
            return Math.Round(Decimal.Subtract(NetProfitAfterTax, GPACapital), 2);
        }
        public decimal CumulativeCashFlow(decimal PreviousCumulativeCashFlow, decimal CurrentCashFlow)
        {
            return Math.Round(Decimal.Subtract(CurrentCashFlow, PreviousCumulativeCashFlow), 2);
        }
        public decimal PresentValue(decimal FreeCashFlow, decimal DiscountRate, decimal StartingYear, decimal CurrentYear)
        {
            try
            {
                Double DiscountRatePercent = Convert.ToDouble(Decimal.Divide(DiscountRate, 100));
                Decimal Divider = Convert.ToDecimal(Math.Pow(DiscountRatePercent, Convert.ToDouble(Decimal.Subtract(CurrentYear, StartingYear))));
                return Math.Round(Decimal.Divide(FreeCashFlow, Divider), 2);
            }
            catch (DivideByZeroException ex)
            {
                return 0.00M;
            }
        }

        public decimal TotalsSum(List<decimal> FinancialTotals)
        {
            return Math.Round(FinancialTotals.Sum(), 2);
        }
        public decimal TotalsSum(List<int> FinancialTotals)
        {
            return FinancialTotals.Sum();
        }
        #endregion
    }
}
