using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class GateUploadedDocumentation : UploadedDocumentation
    {
        public int GateId { get; set; }

        [ForeignKey("GateId")]
        public virtual Gate Gate { get; set; }
    }
}
