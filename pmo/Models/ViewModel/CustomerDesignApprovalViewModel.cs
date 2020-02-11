using CustomValidators;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Helpers;

namespace ViewModels
{
    public class CustomerDesignApprovalViewModel: CustomerDesignApproval
    {
        public List<CustomerDesignApprovalViewModel> Versions { get; set; }

        [UserExistsActiveDirectory(Helpers.ErrorMessages.MissingUserActiveDirectory)]
        [Required(ErrorMessage = ErrorMessages.Required)]  
        public new string SentForApprovalBy { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime DateSentForApprove { get; set; }

        [UserExistsActiveDirectory(Helpers.ErrorMessages.MissingUserActiveDirectory)]
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ApprovedBy { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime ApprovedDate { get; set; }
          
        public new virtual Stage Stage { get; set; }

        public new virtual List<CustomerDesignApprovalUploadedDocumentation> ImportantDocumentation { get; set; }


    }
}
