using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class BaseProjectDetail : DatabaseModel {
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
    }
}
