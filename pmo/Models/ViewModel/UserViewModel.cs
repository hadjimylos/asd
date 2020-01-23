using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ViewModels {
    public class UserViewModel : dbModels.User {
        public bool isCreate { get; set; }
        [Required(ErrorMessage = Helpers.ErrorMessages.Required)]
        public new string NetworkUsername { get; set; }

        [Required(ErrorMessage = Helpers.ErrorMessages.Required)]
        public new string RoleId { get; set; }

        public SelectList RoleList { get; set; }
        public MultiSelectList CitizenshipsList { get; set; }

        private List<int> _userCitizenships = new List<int>();

        [Required(ErrorMessage = Helpers.ErrorMessages.Required)]
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
