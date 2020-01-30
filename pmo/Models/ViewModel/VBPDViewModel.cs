using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace ViewModels
{
    public class VBPDViewModel : ProjectDetail
    {
        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(40, ErrorMessage = ErrorMessages.MinimumValue)]
        public string Name { set; get; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [StringLength(10, ErrorMessage = ErrorMessages.MinimumValue)]
        public new string Salesforce { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.AtLeastOne)]
        public new int ProjectCategoryTagId { get; set; }
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.AtLeastOne)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProductLineTagId { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.AtLeastOne)]
        public new int ProjectClassificationTagId { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int ProjectId { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string EngineeringChecklistUrl { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.AtLeastOne)]
        public new int DesignAuthorityTagId { get; set; }

        public new Project Project { get; set; }
        public new Tag ProjectClassification { get; set; }
        public new Tag ProjectCategory { get; set; }
        public new Tag ProductLine { get; set; }
        public new Tag DesignAuthority { get; set; }
        public new List<ProjectDetail_SalesRegion> SalesRegions { get; set; }
        public new List<ProjectDetail_EndUserCountry> EndUseCountries { get; set; }
        public new List<ProjectDetail_Customer> Customers { get; set; }

        public SelectList ProjectCategoryTagDropDown { set; get; }
        public SelectList ProjectClassificationDropDown { set; get; }
        public SelectList ProductLineDropDown { set; get; }
        public SelectList DesignAuthorityDropDown { set; get; }
    }
}
