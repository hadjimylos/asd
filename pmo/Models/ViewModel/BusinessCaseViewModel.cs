using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Helpers;

namespace ViewModels
{
    public class BusinessCaseViewModel : BusinessCase
    {

        public List<BusinessCaseViewModel> Versions { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]

        public List<FinancialDataViewModel> FinancialDataViewModel { get; set; }


        public new int StageId { get; set; }
        public new virtual Stage Stage { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new bool WillCustomerFundQual { get; set; }     
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new bool WillCustomerFundTooling { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal ProbabiltyOfWin { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal TargetFirstYearGrossMargin { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime DataStartingDate { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int NumberOfYears { get; set; }
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
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new bool Changes { get; set; }

        public decimal NPV { get; set; }
        public decimal ROI { get; set; }
        public decimal PaybackPeriodYears { get; set; }


        public new virtual List<BusinessCase_ManufacturingLocation> ManufacturingLocations { get; set; }

        public MultiSelectList ManufacturingLocationsDropDown { set; get; }
        [Required, MinLength(1, ErrorMessage = Helpers.ErrorMessages.AtLeastOne)]
        public List<int> ManufacturingLocationsIds { get; set; }

    }
}
