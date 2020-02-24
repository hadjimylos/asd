using CustomValidators;
using dbModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class InvestmentPlanForm : InvestmentPlan {

        public List<InvestmentPlanForm> Versions { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ItemNumber { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Item { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string PurchasedFrom { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ShipToLocation { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Cost { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Terms { get; set; }
    }
}
