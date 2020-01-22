using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public class StageConfig_RequiredSchedule : DatabaseModel {

        public int RequiredScheduleTagId { get; set; }

        [ForeignKey("RequiredScheduleTagId")]
        public virtual Tag RequiredSchedule { get; set; }

        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual StageConfig StageConfig { get; set; }

    }
}