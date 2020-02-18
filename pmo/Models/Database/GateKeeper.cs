namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;

    public class GateKeeper : DatabaseModel {
        public int GateKeeperConfigId { get; set; }
        
        [ForeignKey("GateKeeperConfigId")]
        public virtual GateKeeperConfig GateKeeperConfig { get; set; }

        public string GateKeeperName { get; set; }
        
        public int GateId { get; set; }
        
        [ForeignKey("GateId")]
        public virtual Gate Gate { get; set; }
    }
}
