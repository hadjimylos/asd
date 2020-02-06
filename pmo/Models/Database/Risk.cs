using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class Risk: DatabaseModel
    {
        public string Name { get; set; }

        public decimal RiskPropability { get; set; }

        public int RiskTypeTagId { get; set; }
        
        [ForeignKey("RiskTypeTagId")]
        public virtual Tag RiskType { get; set; }

        public int RiskImpactTagId { get; set; }
        
        [ForeignKey("RiskImpactTagId")]
        public virtual Tag RiskImpact { get; set; }

        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
    }
}