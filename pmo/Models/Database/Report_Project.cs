using dbModels;
using dbModels.Report;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace dbModels.Report
{
    public class Report_Project: Report_DatabaseModel
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CurrentStageNumber { set; get; }
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
        [Required]
        public string ProjectProcessType { set; get; }
        [Required]
        public string ExportControlCode { set; get; }
        [Required]
        public string EndUseDestinationCountry { set; get; }

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

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public virtual List<Report_ProjectCustomers> Report_Customers { get; set; }
        public virtual List<Report_ProjectSalesRegion> Report_SalesRegions { get; set; }
        public virtual List<Report_ProjectEndUserCountries> Report_EndUseCountries { get; set; }
    }
}
