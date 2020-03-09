using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels.Report
{
    public class Report_ProjectEndUserCountries : Report_DatabaseModel
    {
        public int EndUserCountriesTagId { get; set; }
        public int ProjectId { get; set; }


        [ForeignKey("ProjectId")]
        public virtual Report_Project Report_Project { get; set; }
        [ForeignKey("EndUserCountriesTagId")]
        public virtual Tag EndUserCountries { get; set; }

    }
}