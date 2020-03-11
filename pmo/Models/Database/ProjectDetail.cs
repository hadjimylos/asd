using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ViewModels.Helpers;

namespace dbModels {
    public class ProjectDetail: HistoryModel
    {
        public int ProjectId { get; set; }
        [Required]
        public string Salesforce { get; set; }
        [Required]
        public int ProjectCategoryTagId { get; set; }
        [Required]
        public int ProductLineTagId { get; set; }
        [Required]
        public int ProjectClassificationTagId { get; set; }
        [Required]
        public int ExportApplicationTypeTagId { get; set; }
        [Required]
        public int DesignAuthorityTagId { get; set; }
        public string ExportRestrictedUsers { set; get; }
        [Required]
        public string ExportControlCode { set; get; }       
        [Required]
        public string EndUseDestinationCountry { set; get; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        [ForeignKey("ProductLineTagId")]
        public virtual Tag ProductLine { get; set; }
        [ForeignKey("ProjectClassificationTagId")]
        public virtual Tag ProjectClassification { get; set; }
        [ForeignKey("ProjectCategoryTagId")]
        public virtual Tag ProjectCategory { get; set; }
        [ForeignKey("DesignAuthorityTagId")]
        public virtual Tag DesignAuthority { get; set; }
        [ForeignKey("ExportApplicationTypeTagId")]
        public virtual Tag ExportApplicationType { get; set; }
        public virtual List<ProjectDetail_Customer> Customers { get; set; }
        public virtual List<ProjectDetail_SalesRegion> SalesRegions { get; set; }
        public virtual List<ProjectDetail_EndUserCountry> EndUseCountries { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public string ProjectProcessType { set; get; }
        
    }
}