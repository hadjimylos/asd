using CustomValidators;
using dbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class CustomerDesignApprovalForm : CustomerDesignApproval {

        public List<CustomerDesignApprovalForm> Versions { get; set; }

        [UserExistsActiveDirectory(ErrorMessages.MissingUserActiveDirectory)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string SentForApprovalBy { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime DateSentForApprove { get; set; }

        [UserExistsActiveDirectory(ErrorMessages.MissingUserActiveDirectory)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ApprovedBy { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime ApprovedDate { get; set; }
    }
}
