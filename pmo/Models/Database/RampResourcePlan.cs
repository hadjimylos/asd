using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public enum RampResourcePlanType { Url = 1, Description = 2 }
    
    public class RampResourcePlan: HistoryModel
    {
        public string Value { get; set; }
       
        public RampResourcePlanType Type { get; set; }

        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
    }
}