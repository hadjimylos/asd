using System.Collections.Generic;

namespace dbModels {
    public class GateConfig : DatabaseModel { 
        public int GateNumber { get; set; }

        public virtual List<Role> Role { get; set; }
    }
}
