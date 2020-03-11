using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels.Report
{
    public class Report_BusinessCase_ManufacturingLocations : Report_DatabaseModel
    {
        public int ManufacturingLocationsTagId { get; set; }
        [ForeignKey("ManufacturingLocationsTagId")]
        public virtual Tag ManufacturingLocations { get; set; }

        public int ReportBusinessCaseId { get; set; }
        [ForeignKey("ReportBusinessCaseId")]
        public virtual Report_BusinessCase Report_BusinessCase { get; set; }
    }
}