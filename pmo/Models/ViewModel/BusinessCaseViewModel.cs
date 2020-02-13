using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BusinessCaseViewModel : BusinessCase
    {

        public List<BusinessCaseViewModel> Versions { get; set; }

        public new int StageId { get; set; }
        public new virtual Stage Stage { get; set; }

        public new bool WillCustomerFundQual { get; set; }

        public new bool WillCustomerFundTooling { get; set; }

        public new decimal ProbabiltyOfWin { get; set; }

        public new decimal TargetFirstYearGrossMargin { get; set; }

        public new DateTime DataStartingDate { get; set; }

        public new int NumberOfYears { get; set; }

        public new decimal DiscountRate { get; set; }

        public new decimal TaxRate { get; set; }

        public new string GpaRequirements { get; set; }

        public new decimal MultipleFieldsGeneratedTable { get; set; }

        public new string ProjectScope { get; set; }

        public new decimal WorkRequirementAmount { get; set; }

        public new string WorkRequirementUrl { get; set; }

        public new bool StrategicAlignment { get; set; }

        public new string AddToTecnicalAbilities { get; set; }

        public new DateTime ProjectCompletion { get; set; }

        public new decimal TimeFromProjectCompletionToRevenueGeneration { get; set; }

        public new string CustomerMarketAnalysis { get; set; }

        public new bool Changes { get; set; }

        public new virtual List<BusinessCase_ManufacturingLocation> ManufacturingLocations { get; set; }

        public MultiSelectList ManufacturingLocationsDropDown { set; get; }
        public List<int> ManufacturingLocationsIds { get; set; }

    }
}
