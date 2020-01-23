using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class GateKeeperConfig : DatabaseModel {
        [Required]
        public string Keeper { get; set; }

        public int GateConfigId { get; set; }

        [ForeignKey("GateConfigId")]
        public virtual GateConfig GateConfig { get; set; }
    }
}
