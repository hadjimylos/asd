namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;

    public class LiteStageFileConfig : DatabaseModel {
        public int RequiredFileTagId { get; set; }

        [ForeignKey("RequiredFileTagId")]
        public virtual Tag RequiredFile { get; set; }

        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual LiteStageConfig StageConfig { get; set; }
    }
}
