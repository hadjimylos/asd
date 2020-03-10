namespace dbModels {
    using System.Collections.Generic;

    public class LiteStageConfig : BaseStageConfig {
        public virtual List<LiteRequiredSchedule> RequiredSchedules { get; set; }
        public virtual List<LiteGateKeeperConfig> GateKeeperConfigs { get; set; }
    }
}
