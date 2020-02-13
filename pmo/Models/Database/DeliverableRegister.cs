using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class DeliverableRegister : DatabaseModel {
        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }

        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag DeliverableRegisterTag { get; set; }

        public string FileName { get; set; }

        public string FileUrl { get; set; }
    }
}
