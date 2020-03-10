namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;

    public class LiteStageFileConfig : BaseStageFileConfig {
        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual LiteStageConfig StageConfig { get; set; }
    }
}
