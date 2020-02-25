using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class BusinessCaseForm : BusinessCase {
        public List<BusinessCaseForm> Versions { get; set; }

        public new bool WillCustomerFundQual { get; set; }
        public new bool WillCustomerFundTooling { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal ProbabiltyOfWin { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal TargetFirstYearGrossMargin { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int FinancialStartYear { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal DiscountRate { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal TaxRate { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal MultipleFieldsGeneratedTable { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal WorkRequirementAmount { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new bool StrategicAlignment { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime ProjectCompletion { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal TimeFromProjectCompletionToRevenueGeneration { get; set; }
        public new bool Changes { get; set; }

        public MultiSelectList ManufacturingLocationsDropDown { set; get; }
        [Required, MinLength(1, ErrorMessage = ErrorMessages.AtLeastOne)]
        public List<int> ManufacturingLocationsIds { get; set; }

    }
}
