namespace ViewModels {
    using dbModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GateViewModel : Gate {
        public GateViewModel() {
            this.GateKeepersNew = new List<GateKeeperViewModel>();
        }

        public List<GateKeeperViewModel> GateKeepersNew { get; set; }
        
        [Required(ErrorMessage = Helpers.ErrorMessages.Required)]
        public new DateTime? ActualReviewDate { get; set; }

        [Required(ErrorMessage = Helpers.ErrorMessages.Required)]
        public new string Comments { get; set; }
    }
}
