using CustomValidators;
using dbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace ViewModels
{
    public class ProductIntroChecklistViewModel:ProductIntroChecklist
    {
        public List<ProductIntroChecklistViewModel> Versions { get; set; }

        public new bool IsMarketingRequired { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Filename { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [UserExistsActiveDirectory(Helpers.ErrorMessages.MissingUserActiveDirectory)]
        public new string ApprovedBy { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime ApprovedByDate { get; set; }

        public virtual Stage Stage { get; set; }
    }
}
