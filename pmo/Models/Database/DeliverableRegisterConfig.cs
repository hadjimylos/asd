using dbModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class DeliverableRegisterConfig : DatabaseModel {
        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual StageConfig StageConfig { get; set; }

        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag DeliverableRegisterTag { get; set; }
    }
}
