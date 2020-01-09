using System;
using System.Collections.Generic;

namespace dbModels
{
    public class ProductInfrigmentPatentability:DatabaseModel {
        public bool ContainsInfingmentIssues { get; set; }

        public string PatentNumber { get; set; }

        public string Issue { get; set; }

        public string MitigationStrategy { get; set; }

        public bool ContainsFeaturesRequiringIPProtection { get; set; }

        public bool InventionDisclosureSubmitted { get; set; }

        public DateTime ProductFirstTimeOfferedForSale { get; set; }

        public virtual List<UploadedDocumentation> ImportantDocumentation { get; set; }
    }
}