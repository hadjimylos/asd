using dbModels;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms { 
    public class RoleForm : Role {
        public bool isCreate { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string FriendlyName { get; set; }
    }
}