using CustomValidators;
using dbModels;
using System.ComponentModel.DataAnnotations;

namespace ViewModels {
    public class GateKeeperViewModel : GateKeeper {
        public string Label { get; set; }
        
        [Required(ErrorMessage = Helpers.ErrorMessages.Required)]
        [UserExistsActiveDirectory(errorMessage: Helpers.ErrorMessages.MissingUserActiveDirectory)]
        public new string GateKeeperName { get; set; }
    }
}
