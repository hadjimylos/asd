using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels.Report
{
    public class Report_BusinessCase_ManufacturingLocations : Report_DatabaseModel
    {
        public int ManufacturingLocationsTagId { get; set; }

        [ForeignKey("ManufacturingLocationsTagId")]
        public virtual Tag ManufacturingLocations { get; set; }

        public int BusinessCaseId { get; set; }

        [ForeignKey("BusinessCaseId")]
        public virtual Report_BusinessCase Report_BusinessCase { get; set; }
    }
}