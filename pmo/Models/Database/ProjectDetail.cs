using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class ProjectDetail: HistoryModel
    {
        public int ProjectId { get; set; }
        [Required]
        [StringLength(10)]
        public string Salesforce { get; set; }
        [Required]
        public string EngineeringChecklistUrl { get; set; }
        [Required]
        public int ProjectCategoryTagId { get; set; }
        [Required]
        public int ProductLineTagId { get; set; }
        [Required]
        public int ProjectClassificationTagId { get; set; }
        //[Required]
        public int ExportApplicationTypeTagId { get; set; }
        [Required]
        public int DesignAuthorityTagId { get; set; }
        [Required]
        [StringLength(30)]
        public string ProjectProcessType { set; get; }
        [Required]
        [StringLength(50)]
        public string ExportControlCode { set; get; }
        [Required]
        [StringLength(50)]
        public string ExportRestrictedUsers{ set; get; }
        [Required]
        public int CustomerTagId { get; set; }
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
        [ForeignKey("CustomerTagId")]
        public virtual Tag Customer { get; set; }

        public virtual List<ProjectDetail_SalesRegion> SalesRegions { get; set; }
        public virtual List<ProjectDetail_EndUserCountry> EndUseCountries { get; set; }
        public virtual List<ProjectDetail_Customer> Customers { get; set; }
    }
}