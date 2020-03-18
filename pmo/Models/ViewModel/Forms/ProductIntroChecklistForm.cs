using CustomValidators;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms
{
    public class ProductIntroChecklistForm: ProductIntroChecklist
    {
        public List<ProductIntroChecklistForm> Versions { get; set; }

        [Display(Name = "Is Marketing Required")]
        public new bool IsRequired { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime ApprovedByDate { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public IFormFile File { get; set; }


        public SelectList UsersDropDown { get; set; }

    }
}
