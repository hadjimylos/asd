using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class TagForm : Tag {

        public bool isCreate { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int TagCategoryId { get; set; }

        public SelectList TagCategorySelectList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Name { get; set; }
    }
}