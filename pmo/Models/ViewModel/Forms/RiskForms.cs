using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms { 
    public class RiskForm : Risk {
        public SelectList RiskTypeList { get; set; }

        public SelectList RiskImpactList { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required), Range(0.00, 100.00, ErrorMessage = ErrorMessages.ValueBetween0And100)]
        public new decimal RiskPropability { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.Required)]
        public new int RiskTypeTagId { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.Required)]
        public new int RiskImpactTagId { get; set; }
    }
}