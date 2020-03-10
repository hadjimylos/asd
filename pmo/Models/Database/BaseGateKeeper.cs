namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;

    public class BaseGateKeeper : DatabaseModel {
        public string GateKeeperName { get; set; }

        public int GateId { get; set; }

        [ForeignKey("GateId")]
        public virtual Gate Gate { get; set; }
    }
}
