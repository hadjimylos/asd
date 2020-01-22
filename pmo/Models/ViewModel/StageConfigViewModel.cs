using dbModels;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace ViewModels {
    public class StageConfigViewModel : StageConfig  {
        public bool isCreate { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int? MinProjectJustifications { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int? MinBusinessCases { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int? MinProductInfringementPatentabilities { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int? MinKeyCharacteristics { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int? MinCustomerDesignApprovals { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int? MinInvestmentPlans { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int? MinProductIntroChecklist { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int? MinRampResourcePlans { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int? MinQualificationTesting { get; set; }
    }
}