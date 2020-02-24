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
        public MultiSelectList CitizenshipsList { get; set; }

        private List<int> _userCitizenships = new List<int>();

        [Required, MinLength(1, ErrorMessage = ErrorMessages.AtLeastOne)]
        public List<int> UserCitizenships
        {
            get
            {
                if (this.Citizenships != null)
                {
                    _userCitizenships = this.Citizenships.Select(x => x.Citizenships.Id).ToList();
                    return _userCitizenships;
                }
                return _userCitizenships;
            }
            set 
            {
                _userCitizenships = value;
            } 
        }


    }
}
