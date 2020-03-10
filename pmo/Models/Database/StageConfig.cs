namespace dbModels {
    using System.Collections.Generic;
    public class StageConfig : BaseStageConfig {
        public virtual List<StageConfig_RequiredSchedule> RequiredSchedules { get; set; }
        public virtual List<GateKeeperConfig> GateKeeperConfigs { get; set; }
        public virtual List<Gate> Gates { get; set; }
    }
}
