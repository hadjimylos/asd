using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class ProjectDetail: HistoryModel {        
        public decimal SalesforceNumber { get; set; }
        
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        
        public bool IsExportConrolRequired { get; set; }
        
        public string EngineeringChecklistUrl { get; set; }

        public int ProjectCategoryTagId { get; set; }
        
        [ForeignKey("ProjectCategoryTagId")]
        public virtual Tag ProjectCategory { get; set; }

        public int ProductLineTagId { get; set; }
        
        [ForeignKey("ProductLineTagId")]
        public virtual Tag ProductLine { get; set; }

        public int ProjectClassificationTagId { get; set; }
        
        [ForeignKey("ProjectClassificationTagId")]
        public virtual Tag ProjectClassification { get; set; }

        public int ExportApplicationTypeTagId { get; set; }
        [ForeignKey("ExportApplicationTypeTagId")]
        public virtual Tag ExportApplicationType { get; set; }

        public int DesignAuthorityTagId { get; set; }
        
        [ForeignKey("DesignAuthorityTagId")]
        public virtual Tag DesignAuthority { get; set; }        
        
        public virtual List<ProjectDetail_SalesRegions> SalesRegions { get; set; }
        
        public virtual List<ProjectDetail_EndUserCountries> EndUseCountries { get; set; }    
        
        public virtual List<ProjectDetail_Customers> Customers { get; set; }
    }
}