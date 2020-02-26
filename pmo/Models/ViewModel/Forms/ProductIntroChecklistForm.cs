using CustomValidators;
using dbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms
{
    public class ProductIntroChecklistForm:ProductIntroChecklist
    {
        public List<ProductIntroChecklistForm> Versions { get; set; }

        [Display(Name = "Is Marketing Required")]
        public new bool IsMarketingRequired { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Filename { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [UserExistsActiveDirectory(ErrorMessages.MissingUserActiveDirectory)]
        public new string ApprovedBy { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime ApprovedByDate { get; set; }
    }
}
