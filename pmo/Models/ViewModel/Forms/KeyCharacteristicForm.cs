using CustomValidators;
using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms
{
    public class KeyCharacteristicForm : KeyCharacteristic
    {
        public List<KeyCharacteristicForm> Versions { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ItemNumber { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string Requirement { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string MeasuredValue { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new string ExpectedOutcomeRisk { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int StageId { get; set; }
        [Required(ErrorMessage = ErrorMessages.Required)]
        [MinValue(1)]
        public new int RequirementSourceId { get; set; }
        public List<SelectListItem> RequirementSourceDropDown { set; get; }
    }
}

