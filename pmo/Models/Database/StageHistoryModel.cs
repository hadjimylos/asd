using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class StageHistoryModel : HistoryModel {
        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
    }
}
