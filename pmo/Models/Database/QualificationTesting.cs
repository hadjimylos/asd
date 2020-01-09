using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class QualificationTesting: HistoryModel
    {
        public bool IsQualificationRequired { get; set; }

        public string Description { get; set; }

        public string AttachmentUrl { get; set; }

        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
    }
}