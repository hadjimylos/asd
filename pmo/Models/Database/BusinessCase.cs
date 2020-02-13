using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class BusinessCase: HistoryModel
    {
        public int StageId { get; set; }
        
        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }

        public bool WillCustomerFundQual { get; set; }

        public bool WillCustomerFundTooling { get; set; }

        public decimal ProbabiltyOfWin { get; set; }

        public decimal TargetFirstYearGrossMargin { get; set; }

        public DateTime DataStartingDate { get; set; }

        public int NumberOfYears { get; set; }

        public decimal DiscountRate { get; set; }

        public decimal TaxRate { get; set; }

        public string GpaRequirements { get; set; }

        public decimal MultipleFieldsGeneratedTable { get; set; }

        public string ProjectScope { get; set; }

        public decimal WorkRequirementAmount { get; set; }

        public string WorkRequirementUrl { get; set; }

        public bool StrategicAlignment { get; set; }

        public string AddToTecnicalAbilities { get; set; }

        public DateTime ProjectCompletion { get; set; }

        public decimal TimeFromProjectCompletionToRevenueGeneration { get; set; }

        public string CustomerMarketAnalysis { get; set; }

        public bool Changes { get; set; }

        public virtual List<BusinessCase_ManufacturingLocation> ManufacturingLocations { get; set; }
    }
}