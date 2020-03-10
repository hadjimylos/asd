namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;

    public class GateKeeper : BaseGateKeeper {
        public int GateKeeperConfigId { get; set; }
        
        [ForeignKey("GateKeeperConfigId")]
        public virtual GateKeeperConfig GateKeeperConfig { get; set; }
    }
}
