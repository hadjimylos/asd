using CustomValidators;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ViewModels.Helpers;

namespace dbModels {
    public class Mitigation : DatabaseModel  {
        public int RiskId { get; set; }
        
        [ForeignKey("RiskId")]
        
        public virtual Risk Risk { get; set; }
        
        [Required]
        public string MitigationPlan { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MinValue(1)]
        public int ResponsibilityUserId { get; set; }
        [ForeignKey("ResponsibilityUserId")]
        public virtual User ResponsibilityUser { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }
    }
}
