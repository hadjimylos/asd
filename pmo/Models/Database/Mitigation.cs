using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class Mitigation:HistoryModel  {
        public int RiskId { get; set; }
        
        [ForeignKey("RiskId")]
        
        public virtual Risk Risk { get; set; }
        
        public string MitigationPlan { get; set; }
        
        public string Responsibility { get; set; }
        
        public DateTime TargetDate { get; set; }
    }
}
