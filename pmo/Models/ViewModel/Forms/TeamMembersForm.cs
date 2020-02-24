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

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Product_Engineering { get; set; }
        public List<SelectListItem> ProductEngineeringList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Advanced_Technology { get; set; }
        public List<SelectListItem> AdvancedTechnologyList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Sales { get; set; }
        public List<SelectListItem> SalesList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Industry_Segment { get; set; }
        public List<SelectListItem> IndustrySegment { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Operations { get; set; }
        public List<SelectListItem> OperationsList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Manufacturing_Engineering { get; set; }
        public List<SelectListItem> ManufacturingEngineeringList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Planning { get; set; }
        public List<SelectListItem> PlanningList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Sourcing { get; set; }
        public List<SelectListItem> SourcingList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Quality { get; set; }
        public List<SelectListItem> QualityList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Laboratory_Testing { get; set; }
        public List<SelectListItem> LaboratoryTestingList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> Finance { get; set; }
        public List<SelectListItem> FinanceList { get; set; }

    }
}