namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;

    public class GateComment : DatabaseModel {
        public int GateId { get; set; }
        [ForeignKey("GateId")]
        public virtual Gate Gate { get; set; }
        public GateDecisionType DecisionType { get; set; }
        public string Comment { get; set; }
    }
}
