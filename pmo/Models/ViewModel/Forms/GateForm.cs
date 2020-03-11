namespace forms {
    using dbModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Helpers;

    public class GateForm : Gate {
        public GateForm() {
            this.GateKeepersNew = new List<GateKeeperForm>();
        }

        public List<GateKeeperForm> GateKeepersNew { get; set; }
        
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new DateTime? ActualReviewDate { get; set; }
    }
}
