using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class GateKeeperConfig : DatabaseModel {
        [Required]
        public string Keeper { get; set; }

        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual StageConfig StageConfig { get; set; }

        public virtual List<GateKeeper> GateKeepers { get; set; }
    }
}
