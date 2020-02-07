using dbModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ViewModels.Helpers;

namespace ViewModels
{
    public class InvestmentPlanViewModel:InvestmentPlan
    {
        public List<InvestmentPlanViewModel> Versions { get; set; }
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

        public new virtual Stage Stage { get; set; }
    }
}

