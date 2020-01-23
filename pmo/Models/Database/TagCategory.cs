using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace dbModels {
    public class TagCategory: DatabaseModel {
        public string FriendlyName { get; set; }
        
        public string Key { get; set; }
        
        public bool IsFixed { get; set; }
        
        //[ForeignKey("TagId")]
        public virtual List<Tag> Tags { get;set; }

        public List<SelectListItem> GetListItems(List<int> selected) =>
            this.Tags.Select(s => new SelectListItem {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Selected = selected.Contains(s.Id),
                }).ToList();
    }
}
