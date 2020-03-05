using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class Mitigation : DatabaseModel  {
        public int RiskId { get; set; }
        
        [ForeignKey("RiskId")]
        
        public virtual Risk Risk { get; set; }
        
        [Required]
        public string MitigationPlan { get; set; }

        [Required]
        public string Responsibility { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }
    }
}
