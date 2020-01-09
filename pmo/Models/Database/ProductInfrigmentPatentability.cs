using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public class ProductInfrigmentPatentability: HistoryModel
    {
        public bool ContainsInfingmentIssues { get; set; }

        public string PatentNumber { get; set; }

        public string Issue { get; set; }

        public string MitigationStrategy { get; set; }

        public bool ContainsFeaturesRequiringIPProtection { get; set; }

        public bool InventionDisclosureSubmitted { get; set; }

        public DateTime ProductFirstTimeOfferedForSale { get; set; }

        public virtual List<ProductInfrigmentPatentabilityUploadedDocumentation> ProductInfrigmentPatentabilityImportantDocumentation { get; set; }

        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
    }
}