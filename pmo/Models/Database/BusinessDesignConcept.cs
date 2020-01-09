using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class BusinessDesignConcept: DesignConcept {
        public int BusinessCaseId { get; set; }
        
        [ForeignKey("BusinessCaseId")]
        public virtual BusinessCase BusinessCase { get; set; }        
        
    }
}
