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
        public string Productext { get; set; }
        public List<SelectListItem> ProductDropDown { set; get; }
        public new Tag Product { set; get; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [Display(Name = "Addition to our Technical Capabilities")]
        public new bool AddToInhouseTechnicalAbilities { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int StageId { get; set; }
    }
}
