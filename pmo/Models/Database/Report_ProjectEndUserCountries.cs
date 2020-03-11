using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels.Report
{
    public class Report_ProjectEndUserCountries : Report_DatabaseModel
    {
        public int ReportProjectId { get; set; }
        [ForeignKey("ReportProjectId")]
        public virtual Report_Project Report_Project { get; set; }

        public int EndUserCountriesTagId { get; set; }
        [ForeignKey("EndUserCountriesTagId")]
        public virtual Tag EndUserCountries { get; set; }

    }
}