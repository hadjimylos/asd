using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class ProjectDetail_SalesRegions : DatabaseModel {
        public int SalesRegionTagId { get; set; }

        [ForeignKey("SalesRegionTagId")]
        public virtual Tag SalesRegion { get; set; }

        public int ProjectDetailId { get; set; }

        [ForeignKey("ProjectDetailId")]
        public virtual ProjectDetail ProjectDetail { get; set; }
    }
}
