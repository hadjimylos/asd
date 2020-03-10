using CustomValidators;
using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms
{
    public class VBPDForm : ProjectDetail
    {
        public VBPDForm() {
            CustomerIds = new List<int>();
            SalesRegionIds = new List<int>();

        }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public string Name { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Salesforce { get; set; }

        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProjectCategoryTagId { get; set; }

        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProductLineTagId { get; set; }

        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProjectClassificationTagId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProjectId { get; set; }

        //[Required(ErrorMessage = ErrorMessages.Required)]
        //public new string EngineeringChecklistUrl { get; set; }
        
        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int DesignAuthorityTagId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ExportControlCode { set; get; }

        [MinValue(1)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ExportApplicationTypeTagId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string EndUseDestinationCountry { get; set; }
 
        public SelectList ProjectCategoryTagDropDown { set; get; }
        public SelectList ProjectClassificationDropDown { set; get; }
        public SelectList ProductLineDropDown { set; get; }
        public SelectList DesignAuthorityDropDown { set; get; }
        public SelectList ExportApplicationTypeDropDown { set; get; }


        public MultiSelectList SalesRegionsDropDown { set; get; }
        public MultiSelectList CustomerDropDown { set; get; }
        public List<int> SalesRegionIds { get; set; }
        public List<int> CustomerIds { get; set; }

    }
}
