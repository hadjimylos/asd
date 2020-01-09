using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class Tag : DatabaseModel {
        public string Name { get; set; }
        
        public int TagCategoryId { get; set; }
        
        [ForeignKey("TagCategoryId")]
        public virtual TagCategory TagCategory { get; set; }
    }
}
