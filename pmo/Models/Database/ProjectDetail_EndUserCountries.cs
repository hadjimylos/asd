using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class ProjectDetail_EndUserCountries: DatabaseModel {
        public int EndUserCountriesTagId { get; set; }

        [ForeignKey("EndUserCountriesTagId")]
        public virtual Tag EndUserCountries { get; set; }

        public int ProjectDetailId { get; set; }

        [ForeignKey("ProjectDetailId")]
        public virtual ProjectDetail ProjectDetail { get; set; }
    }
}
