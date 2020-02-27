namespace forms
{
    using CustomValidators;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class StageFileForm
    {
        public List<IFormFile> stageFiles { get; set; }
        public string Description { get; set; }
        [MinValue(1, ErrorMessage = "This is a required field")]
        public int TagId { get; set; }
        public string TagDescription { get; set; }
        public List<SelectListItem> FileUploadDropdown { get; set; }
    }
}
