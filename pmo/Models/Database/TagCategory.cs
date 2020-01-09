using System.Collections.Generic;

namespace dbModels {
    public class TagCategory: DatabaseModel {
        public string FriendlyName { get; set; }
        
        public string Key { get; set; }
        
        public bool IsFixed { get; set; }
        
        //[ForeignKey("TagId")]
        public virtual List<Tag> Tags { get;set; }

    }
}
