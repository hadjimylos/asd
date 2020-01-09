using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class GateApprovers:DatabaseModel {
        public string GateApproverName { get; set; }
        
        public int GateId { get; set; }
        
        [ForeignKey("GateId")]
        public virtual Gate Gate { get; set; }
    }
}
