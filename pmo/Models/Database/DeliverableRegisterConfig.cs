using dbModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class DeliverableRegisterConfig : DatabaseModel {
        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }

        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag DeliverableRegisterTag { get; set; }
    }
}
