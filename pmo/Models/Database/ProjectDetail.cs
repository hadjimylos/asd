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

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        public int DesignAuthorityTagId { get; set; }
        [ForeignKey("ProductLineTagId")]
        public virtual Tag ProductLine { get; set; }
        [ForeignKey("ProjectClassificationTagId")]
        public virtual Tag ProjectClassification { get; set; }
        [ForeignKey("ProjectCategoryTagId")]
        public virtual Tag ProjectCategory { get; set; }
        [ForeignKey("DesignAuthorityTagId")]
        public virtual Tag DesignAuthority { get; set; }

        public virtual List<ProjectDetail_SalesRegion> SalesRegions { get; set; }
        public virtual List<ProjectDetail_EndUserCountry> EndUseCountries { get; set; }
        public virtual List<ProjectDetail_Customer> Customers { get; set; }
    }
}