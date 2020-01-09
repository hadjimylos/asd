using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public class StageConfig_RequiredSchedules: DatabaseModel {

        public int RequiredSchedulesTagId { get; set; }

        [ForeignKey("RequiredSchedulesTagId")]
        public virtual Tag RequiredSchedules { get; set; }

        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual StageConfig StageConfig { get; set; }

    }
}