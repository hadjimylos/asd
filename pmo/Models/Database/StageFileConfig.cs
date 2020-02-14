namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;

    public class StageFileConfig : DatabaseModel {
        public int RequiredFileTagId { get; set; }

        [ForeignKey("RequiredScheduleTagId")]
        public virtual Tag RequiredFile { get; set; }

        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual StageConfig StageConfig { get; set; }
    }
}
