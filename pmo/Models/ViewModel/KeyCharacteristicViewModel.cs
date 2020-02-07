using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace ViewModels
{
    public class KeyCharacteristicViewModel : KeyCharacteristic
    {
        public List<KeyCharacteristicViewModel> Versions { get; set; }

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
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ErrorMessages.Required)]

        public new int RequirementSourceId { get; set; }
        public string RequirementSourceText { get; set; }
        public List<SelectListItem> RequirementSourceDropDown { set; get; }

        public new Tag RequirementSource { set; get; }
        public new Stage Stage { get; set; }
    }

}
