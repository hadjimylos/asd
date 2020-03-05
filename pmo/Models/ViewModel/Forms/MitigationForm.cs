namespace forms {
    using CustomValidators;
    using dbModels;
    using System;
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Helpers;

    public class MitigationForm : Mitigation {
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string MitigationPlan { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [UserExistsActiveDirectory(errorMessage: ErrorMessages.MissingUserActiveDirectory)]
        public new string Responsibility { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MinValue("2019-01-01")]
        public new DateTime TargetDate { get; set; }
    }
}
