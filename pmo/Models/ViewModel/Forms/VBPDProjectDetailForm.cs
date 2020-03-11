using CustomValidators;
using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class VBPDProjectDetailForm : ProjectDetail {
        public List<VBPDProjectDetailForm> Versions { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string EndUseDestinationCountry { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Salesforce { get; set; }
        
        //[Required(ErrorMessage = ErrorMessages.Required)]
        //public new string EngineeringChecklistUrl { get; set; }           
        
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ExportControlCode { set; get; }

        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProjectCategoryTagId { get; set; }
        public List<SelectListItem> ProjectCategoryTagDropDown { set; get; }


        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProjectClassificationTagId { get; set; }
        public List<SelectListItem> ProjectClassificationDropDown { set; get; }

        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProductLineTagId { get; set; }
        public List<SelectListItem> ProductLineDropDown { set; get; }

        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int DesignAuthorityTagId { get; set; }
        public List<SelectListItem> DesignAuthorityDropDown { set; get; }

        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ExportApplicationTypeTagId { get; set; }
        public List<SelectListItem> ExportApplicationTypeDropDown { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> SalesRegionIds { get; set; }
        public List<SelectListItem> SalesRegionsDropDown { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> CustomerIds { get; set; }
        public List<SelectListItem> CustomerDropDown { set; get; }
        public SelectList ProcessDropDown { set; get; }
        public string ProjectProcessTypeSelect { set; get; }



    }
}
