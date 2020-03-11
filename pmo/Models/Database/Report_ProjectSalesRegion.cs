using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels.Report
{
    public class Report_ProjectSalesRegion : Report_DatabaseModel
    {
        public int ReportProjectId { get; set; }
        [ForeignKey("ReportProjectId")]
        public virtual Report_Project Report_Project { get; set; }

        public int SalesRegionTagId { get; set; }
        [ForeignKey("SalesRegionTagId")]
        public virtual Tag SalesRegion { get; set; }
    }
}
