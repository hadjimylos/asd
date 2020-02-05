using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace ViewModels {
    public class VBPDProjectDetailViewModel : ProjectDetail {
        public List<VBPDProjectDetailViewModel> Versions { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string EndUseDestinationCountry { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(10)]
        public new string Salesforce { get; set; }
        
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string EngineeringChecklistUrl { get; set; }
        
        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(30)]
        public new string ProjectProcessType { set; get; }
        
        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(50)]
        public new string ExportControlCode { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.Required)]
        public new int ProjectCategoryTagId { get; set; }
        public List<SelectListItem> ProjectCategoryTagDropDown { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.Required)]
        public new int ProjectClassificationTagId { get; set; }
        public List<SelectListItem> ProjectClassificationDropDown { set; get; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.Required)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProductLineTagId { get; set; }
        public List<SelectListItem> ProductLineDropDown { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.Required)]
        public new int DesignAuthorityTagId { get; set; }
        public List<SelectListItem> DesignAuthorityDropDown { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.Required)]
        public new int ExportApplicationTypeTagId { get; set; }
        public List<SelectListItem> ExportApplicationTypeDropDown { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> SalesRegionIds { get; set; }
        public List<SelectListItem> SalesRegionsDropDown { set; get; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public List<int> CustomerIds { get; set; }
        public List<SelectListItem> CustomerDropDown { set; get; }
    }
}
