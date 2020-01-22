using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class ProjectDetail_Customer : DatabaseModel{
        public int CustomersTagId { get; set; }
        
        [ForeignKey("CustomersTagId")]
        public virtual Tag Customers { get; set; }

        public int ProjectDetailId { get; set; }

        [ForeignKey("ProjectDetailId")]
        public virtual ProjectDetail ProjectDetail { get; set; }

    }
}