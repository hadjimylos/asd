using dbModels;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace ViewModels { 
    public class RoleViewModel:Role {
        public bool isCreate { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string FriendlyName { get; set; }
    }
}