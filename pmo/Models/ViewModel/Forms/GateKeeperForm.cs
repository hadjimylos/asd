using CustomValidators;
using dbModels;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class GateKeeperForm : GateKeeper {
        public string Label { get; set; }
        
        [Required(ErrorMessage = ErrorMessages.Required)]
        [UserExistsActiveDirectory(errorMessage: ErrorMessages.MissingUserActiveDirectory)]
        public new string GateKeeperName { get; set; }
    }
}
