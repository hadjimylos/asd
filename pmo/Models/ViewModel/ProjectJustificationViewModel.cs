using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ProjectJustificationViewModel : ProjectJustification
    {

        public List<ProjectJustificationViewModel> Versions { get; set; }

        public string Productext { get; set; }
        public List<SelectListItem> ProductDropDown { set; get; }

        public new Tag Product { set; get; }

        [Required(ErrorMessage = Helpers.ErrorMessages.Required)]

        public new   bool AddToInhouseTechnicalAbilities { get; set; }
        [Required(ErrorMessage = Helpers.ErrorMessages.Required)]

        public new   int StageId { get; set; }


    }
}
