using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class LiteRequiredSchedule : DatabaseModel {
        public int RequiredScheduleTagId { get; set; }

        [ForeignKey("RequiredScheduleTagId")]
        public virtual Tag RequiredSchedule { get; set; }

        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual LiteStageConfig StageConfig { get; set; }

    }
}
