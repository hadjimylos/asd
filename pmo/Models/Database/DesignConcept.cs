using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    // list of documents uploaded to SharePoint
    public class DesignConcept : DatabaseModel {
        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }

        public string Upload { get; set; }
    }
}
