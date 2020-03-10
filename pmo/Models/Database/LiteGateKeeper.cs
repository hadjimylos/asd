namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;
        public class GateKeeperLite : BaseGateKeeper {
            public int GateKeeperConfigId { get; set; }

            [ForeignKey("GateKeeperConfigId")]
            public virtual LiteGateKeeperConfig GateKeeperConfig { get; set; }
        }
}
