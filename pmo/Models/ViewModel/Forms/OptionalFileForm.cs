namespace forms {
    using dbModels;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Helpers;

    public class OptionalFileForm { 
        public IFormFile File { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int FileTagId { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
