namespace forms {
    using CustomValidators;
    using dbModels;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Helpers;

    public class MitigationForm : Mitigation {
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string MitigationPlan { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MinValue("2019-01-01")]
        public new DateTime TargetDate { get; set; }

        public SelectList UsersDropDown { get; set; }
    }
}
