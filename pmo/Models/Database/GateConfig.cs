using System.Collections.Generic;

namespace dbModels {
    public class GateConfig : DatabaseModel { 
        public int GateNumber { get; set; }

        public virtual List<GateKeeperConfig> GateKeeperConfigs { get; set; }

        public virtual List<Gate> Gates { get; set; }
    }
}
