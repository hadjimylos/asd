using dbModels;
using DeepEqual.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;

namespace PMO.UnitTests
{
    [TestClass]
    public class FinancialsUnitTest
    {
        [TestMethod]
        public void FinancialTableRowsCalculationTest()
        {
            var BC = CreateTestBusinessCaseViewModelData_ForFinancialsCalculation();
            BC.FinancialDataViewModel = Financials.GenerateFinancialsCalculations(BC.FinancialDataViewModel);
            var BCResults = Financials.GenerateFinancialsCalculationsBusinessCase(BC);
            var BCExpectedResults = CreateTestBusinessCaseViewModelData_ForFinancialsResults();

            var JsonResults = JsonConvert.SerializeObject(BCResults);
            var JsonExpectedResults = JsonConvert.SerializeObject(BCExpectedResults);

            Assert.IsTrue(BCResults.IsDeepEqual(BCExpectedResults), "Financial Data calculations are correct");
        }

        [TestMethod]
        public void FinancialTableRowsGenerationTest()
        {
            var BC = CreateTestBusinessCaseViewModelData_RowGeneration();
            var Rows = Financials.GetFinancialDataRows(BC);
            Assert.IsTrue(BC.NumberOfYears== Rows.Count, "Year count matches");
            Assert.AreEqual("2020,2021,2022,2023,2024", String.Join(",", Rows.Select(x => x.Year).ToList<int>()), "Year rows generated correctly");
        }


        #region Scenario 1 Data
        public static BusinessCaseViewModel CreateTestBusinessCaseViewModelData_RowGeneration()
        {
            BusinessCaseViewModel model = new BusinessCaseViewModel()
            {
                DataStartingDate = new System.DateTime(2020, 1, 1),
                NumberOfYears = 5,
                DiscountRate = 9.50M,
                TaxRate = 35,
                LaborRate = 100,
                FinancialData = new List<FinancialData>()
            };

            return model;
        }

        public static BusinessCaseViewModel CreateTestBusinessCaseViewModelData_ForFinancialsCalculation()
        {
            BusinessCaseViewModel model = new BusinessCaseViewModel()
            {
                DataStartingDate = new System.DateTime(2020, 1, 1),
                NumberOfYears = 5,
                DiscountRate = 9.50M,
                TaxRate = 35,
                LaborRate = 100,
                FinancialDataViewModel = new List<FinancialDataViewModel>() {
                    new FinancialDataViewModel(){
                        Totals = false,
                        Year = 2020,
                        Quantity = 50,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    },
                    new FinancialDataViewModel(){
                        Totals = false,
                        Year = 2021,
                        Quantity = 100,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    },
                   new FinancialDataViewModel() {
                        Totals = false,
                        Year = 2022,
                        Quantity = 200,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    },
                    new FinancialDataViewModel(){
                        Totals = false,
                        Year = 2023,
                        Quantity = 275,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    },
                    new FinancialDataViewModel(){
                        Totals = false,
                        Year = 2024,
                        Quantity = 300,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    }

                }
            };

            return model;

        }

        public static BusinessCaseViewModel CreateTestBusinessCaseViewModelData_ForFinancialsResults()
        {
            BusinessCaseViewModel model = new BusinessCaseViewModel()
            {
                DataStartingDate = new System.DateTime(2020, 1, 1),
                NumberOfYears = 5,
                DiscountRate = 9.50M,
                TaxRate = 35,
                LaborRate = 100,
                NPV = 270942.88M,
                ROI = 19.71M,
                PaybackPeriodYears = 3.35M,
                FinancialDataViewModel = new List<FinancialDataViewModel>() {
                    new FinancialDataViewModel(){
                        Totals = false,
                        Year = 2020,
                        Quantity = 50,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                        //Calculated Fields
                        CostExtended = 100000,
                        RevenueExtended=225000,
                        StdMarginEstimatedDollar=125000,
                        StdMarginEstimatedPercent = 55.56M,
                        TotalExpenses=305000,
                        NetProfitBeforeTax=100000,
                        NetProfitAfterTax = 65000,
                        FreeCashFlow=-135000,
                        PresentValue=-135000,
                        CumulativeCashFlow = -135000,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    },
                    new FinancialDataViewModel(){
                        Totals = false,
                        Year = 2021,
                        Quantity = 100,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                         //Calculated Fields
                        CostExtended = 200000,
                        RevenueExtended=450000,
                        StdMarginEstimatedDollar=250000,
                        StdMarginEstimatedPercent = 55.56M,
                        TotalExpenses=305000,
                        NetProfitBeforeTax=225000,
                        NetProfitAfterTax = 146250,
                        FreeCashFlow=-53750,
                        PresentValue=-49086.76M,
                        CumulativeCashFlow = -188750.00M,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    },
                   new FinancialDataViewModel() {
                        Totals = false,
                        Year = 2022,
                        Quantity = 200,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                         //Calculated Fields
                        CostExtended = 400000,
                        RevenueExtended=900000,
                        StdMarginEstimatedDollar=500000,
                        StdMarginEstimatedPercent = 55.56M,
                        TotalExpenses=305000,
                        NetProfitBeforeTax=475000,
                        NetProfitAfterTax = 308750,
                        FreeCashFlow=108750,
                        PresentValue=90698.69M,
                        CumulativeCashFlow = -80000,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    },
                    new FinancialDataViewModel(){
                        Totals = false,
                        Year = 2023,
                        Quantity = 275,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                         //Calculated Fields
                        CostExtended = 550000,
                        RevenueExtended=1237500,
                        StdMarginEstimatedDollar=687500,
                        StdMarginEstimatedPercent = 55.56M,
                        TotalExpenses=305000,
                        NetProfitBeforeTax=662500,
                        NetProfitAfterTax = 430625,
                        FreeCashFlow=230625,
                        PresentValue=175656.42M,
                        CumulativeCashFlow = 150625,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    },
                    new FinancialDataViewModel(){
                        Totals = false,
                        Year = 2024,
                        Quantity = 300,
                        StdCostEstimated = 2000,
                        SalesCostEstimated = 4500,
                        GPACapital = 200000,
                        GPAExpense = 80000,
                        QualCosts = 15000,
                        OtherDevelopmentExpenses = 10000,
                         //Calculated Fields
                        CostExtended = 600000,
                        RevenueExtended=1350000,
                        StdMarginEstimatedDollar=750000,
                        StdMarginEstimatedPercent = 55.56M,
                        TotalExpenses=305000,
                        NetProfitBeforeTax=725000,
                        NetProfitAfterTax = 471250,
                        FreeCashFlow=271250,
                        PresentValue=188674.53M,
                        CumulativeCashFlow = 421875.00M,
                        BusinessCase = new BusinessCase()
                        {
                            DataStartingDate = new System.DateTime(2020, 1, 1),
                            NumberOfYears = 5,
                            DiscountRate = 9.50M,
                            TaxRate = 35,
                            LaborRate = 100
                        }
                    },
                    new FinancialDataViewModel(){
                        Totals = true,
                        Year = 0,
                        Quantity = 925,
                        StdCostEstimated = 0,
                        SalesCostEstimated = 0 ,
                        GPACapital = 1000000,
                        GPAExpense = 400000,
                        QualCosts = 75000,
                        OtherDevelopmentExpenses = 50000,                       
                        //Calculated Fields
                        CostExtended = 1850000,
                        RevenueExtended=4162500,
                        StdMarginEstimatedDollar=2312500,
                        StdMarginEstimatedPercent = 55.56M,
                         TotalExpenses = 1525000,
                        NetProfitBeforeTax=2187500,
                        NetProfitAfterTax = 1421875,
                        FreeCashFlow=421875,
                        PresentValue=270942.88M,
                        CumulativeCashFlow = 168750,
                    },

                }
            };

            return model;
        }

        #endregion
    }
}
