using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace ViewModels {
    public class StageConfigViewModel : StageConfig  {

        public StageConfigViewModel() {
            ScheduleIds = new List<int>();
        }

        public bool isCreate { get; set; }

        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinProjectJustifications { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinBusinessCases { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinProductInfringementPatentabilities { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinKeyCharacteristics { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinCustomerDesignApprovals { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinInvestmentPlans { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinProductIntroChecklist { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinRampResourcePlans { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinQualificationTesting { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = ErrorMessages.MinimumValue)]
        public new int MinDesignConcepts { get; set; }

        public List<SelectListItem> SelectSchedules { get; set; }

        public List<int> ScheduleIds { get; set; }

        public List<string> GateKeepers { get; set; }
    }
}