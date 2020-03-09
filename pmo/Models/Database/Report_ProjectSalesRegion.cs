using bModels.Report;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels.Report
{
    public class Report_ProjectSalesRegion : Report_DatabaseModel
    {

        public int SalesRegionTagId { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("SalesRegionTagId")]
        public virtual Tag SalesRegion { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Report_Project Report_Project { get; set; }
    }
}
