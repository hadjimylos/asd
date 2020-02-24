namespace forms {
    using dbModels;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Helpers;

    public class TagCategoryForm : TagCategory {
        public bool isCreate { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string FriendlyName { get; set; }
        public MultiSelectList TagsList { get; set; }
        public List<int> TagIds { get; set; }
    }
}
