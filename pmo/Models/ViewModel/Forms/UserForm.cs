using dbModels;
﻿using CustomValidators;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ViewModels.Helpers;

namespace forms {
    public class UserForm : User {
        public bool isCreate { get; set; }
        
        [Required(ErrorMessage = ErrorMessages.Required)]
        [UserExistsActiveDirectory(ErrorMessages.MissingUserActiveDirectory)]
        public new string NetworkUsername { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string RoleId { get; set; }

        public SelectList RoleList { get; set; }



    }
}
