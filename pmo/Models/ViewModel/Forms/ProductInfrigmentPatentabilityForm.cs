using dbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms
{
    public class ProductInfrigmentPatentabilityForm:ProductInfrigmentPatentability
    {
        public List<ProductInfrigmentPatentabilityForm> Versions { get; set; }
        
        public new bool ContainsInfingmentIssues { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string PatentNumber { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Issue { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string MitigationStrategy { get; set; }

        public new bool ContainsFeaturesRequiringIPProtection { get; set; }

        public new bool InventionDisclosureSubmitted { get; set; }

        public new DateTime ProductFirstTimeOfferedForSale { get; set; }
    }
}
