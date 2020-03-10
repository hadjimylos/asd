namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;

    public class StageFileConfig : BaseStageFileConfig {
        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual StageConfig StageConfig { get; set; }
    }
}
