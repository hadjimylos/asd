using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels.Report
{
    public class Report_ProjectCustomers : Report_DatabaseModel
    {
        public int CustomersTagId { get; set; }
        [ForeignKey("CustomersTagId")]
        public virtual Tag Customers { get; set; }

        public int ReportProjectId { get; set; }
        [ForeignKey("ReportProjectId")]
        public virtual Report_Project Report_Project { get; set; }
    }
}
