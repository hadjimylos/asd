namespace forms {
    using dbModels;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Helpers;

    public class OptionalFileForm : OptionalFile
    {
        public IFormFile File { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int FileTagId { get; set; }
    }
}
