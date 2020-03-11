using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ViewModels.Helpers;

namespace forms
{
    public class ProjectJustificationForm: ProjectJustification
    {
        public List<ProjectJustificationForm> Versions { get; set; } 
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int StageId { get; set; }

        public List<SelectListItem> AddToInhouseTechnicalAbilitiesDropDown { set; get; }
        [Display(Name = "Addition to our Technical Capabilities")]
        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.Required)]
        public new int AddToInhouseTechnicalAbilitiesTagId { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string CustomerMotivation { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Application { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string DrawingSpecStandards { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string RegulatoryRequirements { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string OtherRequirements { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string TechnicalFeasibility { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Scope { get; set; }

        //Non-required
        public new bool SingleUseProduct { get; set; }

    }
}
