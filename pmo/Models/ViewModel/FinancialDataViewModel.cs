using dbModels;
﻿using CustomValidators;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ViewModels {
    public class FinancialDataViewModel : FinancialData
    {
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
