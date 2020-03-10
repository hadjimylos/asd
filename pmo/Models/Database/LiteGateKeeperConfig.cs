namespace dbModels {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LiteGateKeeperConfig : BaseGateKeeperConfig {
        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual LiteStageConfig StageConfig { get; set; }

        public virtual List<GateKeeper> GateKeepers { get; set; }
    }
}
