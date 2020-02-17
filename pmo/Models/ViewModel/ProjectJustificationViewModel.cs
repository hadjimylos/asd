using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ProjectJustificationViewModel : ProjectJustification
    {

        public List<ProjectJustificationViewModel> Versions { get; set; }

        public string Productext { get; set; }
        public List<SelectListItem> ProductDropDown { set; get; }

        public new Tag Product { set; get; }
    }
}
