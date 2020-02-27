using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModels.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace forms {
    public class TeamMembersForm {

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Program_Manager { get; set; }
        public List<SelectListItem> ProgramManagerList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Product_Manager { get; set; }
        public List<SelectListItem> ProductManagerList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Lead_Engineer { get; set; }
        public List<SelectListItem> LeadEngineerList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Program_Management { get; set; }
        public List<SelectListItem> ProgramManagementList { get; set; }

        public List<int> Product_Engineering { get; set; }
        public List<SelectListItem> ProductEngineeringList { get; set; }

        public List<int> Advanced_Technology { get; set; }
        public List<SelectListItem> AdvancedTechnologyList { get; set; }

        public List<int> Sales { get; set; }
        public List<SelectListItem> SalesList { get; set; }

        public List<int> Industry_Segment { get; set; }
        public List<SelectListItem> IndustrySegment { get; set; }

        public List<int> Operations { get; set; }
        public List<SelectListItem> OperationsList { get; set; }

        public List<int> Manufacturing_Engineering { get; set; }
        public List<SelectListItem> ManufacturingEngineeringList { get; set; }

        public List<int> Planning { get; set; }
        public List<SelectListItem> PlanningList { get; set; }

        public List<int> Sourcing { get; set; }
        public List<SelectListItem> SourcingList { get; set; }

        public List<int> Quality { get; set; }
        public List<SelectListItem> QualityList { get; set; }

        public List<int> Laboratory_Testing { get; set; }
        public List<SelectListItem> LaboratoryTestingList { get; set; }

        public List<int> Finance { get; set; }
        public List<SelectListItem> FinanceList { get; set; }

    }
}