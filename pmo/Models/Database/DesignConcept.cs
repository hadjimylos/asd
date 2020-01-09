using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public enum DesignConceptType { Url=1, FilePath = 2, Link=3 }

    public class DesignConcept:DatabaseModel {
        public int ProjectId { get; set; }
        
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        
        public int DeliverableRegisterId { get; set; }
        
        [ForeignKey("DeliverableRegisterId")]
        public virtual Tag DeliverableRegister { get; set; }
        
        public string Path { get; set; }
        
        public DesignConceptType DesignConceptType { get; set; }
    }
}
