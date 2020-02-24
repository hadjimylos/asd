using AutoMapper;
using dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace ViewModels.Helpers
{
   public static  class Financials
    {
        #region Populate ViewModels
       public static  List<FinancialDataViewModel> GetFinancialDataRows(BusinessCaseViewModel model)
        {
           
                model.FinancialDataViewModel = new List<FinancialDataViewModel>();
                for (int i = 0; i < model.NumberOfYears; i++)
                {
                    model.FinancialDataViewModel.Add(new FinancialDataViewModel()
                    {
                        Totals = false,
                        Year = model.DataStartingDate.Year + i,
                        Quantity = 0,
                        StdCostEstimated = 0,
                        SalesPriceEstimated = 0,
                        GPACapital = 0,
                        GPAExpense = 0,
                        QualCosts = 0,
                        OtherDevelopmentExpenses = 0
                    });
                }                     

            
               
            

            return model.FinancialDataViewModel;
        }

       public static BusinessCaseViewModel GenerateFinancialsCalculationsBusinessCase(BusinessCaseViewModel BusinessCase)
        {
            var Totals = BusinessCase.FinancialDataViewModel.Where(x => x.Totals).First();
            BusinessCase.NPV = Totals.PresentValue;
            BusinessCase.ROI = ROI(Totals.StdMarginEstimatedDollar,BusinessCase.TaxRate, BusinessCase.NumberOfYears, Totals.TotalExpenses);
            BusinessCase.PaybackPeriodYears = PaybackPeriodYears(BusinessCase.FinancialDataViewModel.Where(x=>!x.Totals).ToList());
            return BusinessCase;
        }
        
       public static  List<FinancialDataViewModel> GenerateFinancialsCalculations(List<FinancialDataViewModel> FinancialData)
        {
            FinancialData.Add(new FinancialDataViewModel()
            {
                Totals = true,
                Year = 0,
                Quantity = 0,
                StdCostEstimated = 0,
                SalesPriceEstimated = 0,
                GPACapital = 0,
                GPAExpense = 0,
                QualCosts = 0,
                OtherDevelopmentExpenses = 0
            });
            if (FinancialData.Where(x => !x.Totals).ToList().Count == FinancialData[0].BusinessCase.NumberOfYears)
            {
                var i = 0;
                foreach (var item in FinancialData)
                {
                    if (!item.Totals)
                    {
                        item.CostExtended = CostExtended(item.Quantity, item.StdCostEstimated);
                        item.RevenueExtended = RevenueExtended(item.Quantity, item.SalesPriceEstimated);
                        item.StdMarginEstimatedDollar = StdMarginEstimatedDollar(item.RevenueExtended, item.CostExtended);
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
                            item.CumulativeCashFlow = CumulativeCashFlow(FinancialData[i-1].CumulativeCashFlow, item.FreeCashFlow);
                        }
                        i++;
                    }
                        
                    
                }
                FinancialData.Where(x=>x.Totals).First().CostExtended = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.CostExtended).ToList());
                FinancialData.Where(x=>x.Totals).First().RevenueExtended = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.RevenueExtended).ToList());
                FinancialData.Where(x=>x.Totals).First().StdMarginEstimatedDollar = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.StdMarginEstimatedDollar).ToList());
                FinancialData.Where(x=>x.Totals).First().StdMarginEstimatedPercent = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.StdMarginEstimatedPercent).ToList());
                FinancialData.Where(x=>x.Totals).First().StdMarginEstimatedPercent = TotalsAverage(FinancialData.Where(x => !x.Totals).Select(x => x.StdMarginEstimatedPercent).ToList());
                FinancialData.Where(x=>x.Totals).First().TotalExpenses = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.TotalExpenses).ToList());
                FinancialData.Where(x=>x.Totals).First().NetProfitBeforeTax = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.NetProfitBeforeTax).ToList());
                FinancialData.Where(x=>x.Totals).First().NetProfitAfterTax = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.NetProfitAfterTax).ToList());
                FinancialData.Where(x=>x.Totals).First().FreeCashFlow = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.FreeCashFlow).ToList());
                FinancialData.Where(x=>x.Totals).First().PresentValue = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.PresentValue).ToList());
                FinancialData.Where(x=>x.Totals).First().CumulativeCashFlow = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.CumulativeCashFlow).ToList());
                FinancialData.Where(x=>x.Totals).First().Quantity = Convert.ToInt32(TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.Quantity).ToList()));
                FinancialData.Where(x=>x.Totals).First().StdCostEstimated = 0.00M;
                FinancialData.Where(x=>x.Totals).First().SalesPriceEstimated = 0.00M;
                FinancialData.Where(x => x.Totals).First().GPACapital = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.GPACapital).ToList());
                FinancialData.Where(x => x.Totals).First().GPAExpense = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.GPAExpense).ToList());
                FinancialData.Where(x => x.Totals).First().QualCosts = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.QualCosts).ToList());
                FinancialData.Where(x => x.Totals).First().OtherDevelopmentExpenses = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.OtherDevelopmentExpenses).ToList());
                FinancialData.Where(x => x.Totals).First().TotalExpenses = TotalsSum(FinancialData.Where(x => !x.Totals).Select(x => x.TotalExpenses).ToList());




            }

            return FinancialData;
        }
        #endregion

        #region Calculation Functions

       public static  decimal PaybackPeriodYears(List<FinancialDataViewModel> financialDataViewModel)
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

       public static  decimal ROI (decimal StdMarginEstimatedDollarTotal, decimal TaxRate, int Years, decimal TotalExpences)
        {
            decimal BusinessTaxRatePercent = Decimal.Divide(TaxRate, 100);
            
            var Divide1 = Decimal.Multiply(StdMarginEstimatedDollarTotal, Decimal.Subtract(1,BusinessTaxRatePercent));
            try
            { 
                var Divide2 = Decimal.Divide(Divide1, Years);
                return Math.Round(Decimal.Multiply(Decimal.Divide(Divide2, TotalExpences),100), 2);
            }
            catch (DivideByZeroException ex)
            {
                return 0.00M;
            }
        }
       public static  decimal CostExtended(decimal Quantity, decimal StdCostEstimated)
        {
            return Math.Round(Decimal.Multiply(Quantity, StdCostEstimated), 2);
        }
       public static  decimal RevenueExtended(decimal Quantity, decimal SalesCostEstimated)
        {
            return Math.Round(Decimal.Multiply(Quantity, SalesCostEstimated), 2);
        }
       public static  decimal StdMarginEstimatedDollar(decimal RevenueExtended, decimal CostExtended)
        {
            return Math.Round(Decimal.Subtract(RevenueExtended, CostExtended), 2);
        }
       public static  decimal StdMarginEstimatedPercent(decimal StdMarginEstimatedDollar, decimal RevenueExtended)
        {
            try
            {
                return Math.Round(Decimal.Multiply(Decimal.Divide(StdMarginEstimatedDollar, RevenueExtended),100), 2);
            }
            catch (DivideByZeroException ex)
            {
                return 0.00M;
            }

        }
       public static  decimal TotalExpenses(decimal GPACapital, decimal GPAExpense, decimal QualCosts, decimal OtherDevelopmentExpenses)
        {
            return Math.Round(GPACapital + GPAExpense + QualCosts + OtherDevelopmentExpenses, 2);
        }
       public static  decimal NetProfitBeforeTax(decimal RevenueExtended, decimal CostExtended, decimal QualCosts, decimal OtherDevelopmentExpenses)
        {
            return Math.Round(RevenueExtended - CostExtended - QualCosts - OtherDevelopmentExpenses, 2);
        }
       public static  decimal NetProfitAfterTax(decimal NetProfitBeforeTax, decimal TaxRate)
        {
            decimal BusinessTaxRatePercent = Decimal.Divide(TaxRate, 100);
            return Math.Round(Decimal.Multiply(NetProfitBeforeTax, Decimal.Subtract(1, BusinessTaxRatePercent)), 2);
        }
       public static  decimal FreeCashFlow(decimal NetProfitAfterTax, decimal GPACapital)
        {
            return Math.Round(Decimal.Subtract(NetProfitAfterTax, GPACapital), 2);
        }
       public static  decimal CumulativeCashFlow(decimal PreviousCumulativeCashFlow, decimal CurrentCashFlow)
        {
            return Math.Round(Decimal.Add(CurrentCashFlow, PreviousCumulativeCashFlow), 2);
        }
       public static  decimal PresentValue(decimal FreeCashFlow, decimal DiscountRate, decimal StartingYear, decimal CurrentYear)
        {
            try
            {
                Double DiscountRatePercent = Convert.ToDouble(Decimal.Divide(DiscountRate, 100));
                Decimal Divider = Convert.ToDecimal(Math.Pow(DiscountRatePercent+1, Convert.ToDouble(Decimal.Subtract(CurrentYear, StartingYear))));
                return Math.Round(Decimal.Divide(FreeCashFlow, Divider), 2);
            }
            catch (DivideByZeroException ex)
            {
                return 0.00M;
            }
        }

       public static  decimal TotalsSum(List<decimal> FinancialTotals)
        {
            return Math.Round(FinancialTotals.Sum(), 2);
        }
        public static decimal TotalsAverage(List<decimal> FinancialTotals)
        {
            return Math.Round(Decimal.Divide(FinancialTotals.Sum(), FinancialTotals.Count), 2);
        }
        public static  decimal TotalsSum(List<int> FinancialTotals)
        {
            return FinancialTotals.Sum();
        }
        #endregion
    }
}
