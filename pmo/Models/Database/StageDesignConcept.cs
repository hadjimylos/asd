using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class StageDesignConcept: DesignConcept {
        public int StageId { get; set; }
        
        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }        
        
    }
}
