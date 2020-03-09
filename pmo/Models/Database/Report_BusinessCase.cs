using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels.Report
{
    public class Report_BusinessCase : Report_DatabaseModel
    {
        public int ProjectId { get; set; }

        public bool WillCustomerFundQual { get; set; }

        public bool WillCustomerFundTooling { get; set; }

        public decimal ProbabiltyOfWin { get; set; }

        public decimal TargetFirstYearGrossMargin { get; set; }

        public int FinancialStartYear { get; set; }

        public decimal DiscountRate { get; set; }

        public decimal TaxRate { get; set; }

        public decimal LaborRate { get; set; }

        public string GpaRequirements { get; set; }

        public string ProjectScope { get; set; }

        public decimal WorkRequirementAmount { get; set; }

        public bool StrategicAlignment { get; set; }

        public string AddToTecnicalAbilities { get; set; }

        public DateTime ProjectCompletion { get; set; }

        public decimal TimeFromProjectCompletionToRevenueGeneration { get; set; }

        public string CustomerMarketAnalysis { get; set; }

        public bool Changes { get; set; }

        public decimal GetNPV { set; get; }

        public decimal GetROI { set; get; }

        public decimal GetPaybackPeriod { set;get; }

        [ForeignKey("ProjectId")]
        public virtual Report_Project Report_Project { get; set; }
        public virtual List<Report_BusinessCase_ManufacturingLocations> Report_BusinessCase_ManufacturingLocations { get; set; }
        public virtual List<Report_FinancialData> Report_FinancialData { get; set; }
    }
}