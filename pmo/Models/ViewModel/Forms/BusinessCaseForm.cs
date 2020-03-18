using dbModels;
﻿using CustomValidators;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class BusinessCaseForm : BusinessCase {
        public List<BusinessCaseForm> Versions { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string CustomerMarketAnalysis { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string AddToTecnicalAbilities { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ProjectScope { get; set; }
        
        [Display(Name = "Will Customer Fund Qual?")]
        public new bool WillCustomerFundQual { get; set; }
        [Display(Name = "Will Customer Fund Tooling?")]
        public new bool WillCustomerFundTooling { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [MinValue(minValue: 1)]
        public new decimal ProbabiltyOfWin { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [MinValue(minValue: 1)]
        public new decimal TargetFirstYearGrossMargin { get; set; }
        [MinValue(1700)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int FinancialStartYear { get; set; }
        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal DiscountRate { get; set; }
        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal TaxRate { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public decimal MultipleFieldsGeneratedTable { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxValue(maxValue: 10000)]
        public new decimal WorkRequirementAmount { get; set; }
        
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string GpaRequirements { get; set; }
        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal LaborRate { get; set; }
        [Display(Name = "Strategic Alignment")]
        public new bool StrategicAlignment { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [MinValue(minValue: "1900-01-01")]
        public new DateTime ProjectCompletion { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [MaxValue(100)]
        public new decimal TimeFromProjectCompletionToRevenueGeneration { get; set; }
        public new bool Changes { get; set; }
        public MultiSelectList ManufacturingLocationsDropDown { set; get; }
        [Required, MinLength(1, ErrorMessage = ErrorMessages.AtLeastOne)]
        public List<int> ManufacturingLocationsIds { get; set; }

    }
}
