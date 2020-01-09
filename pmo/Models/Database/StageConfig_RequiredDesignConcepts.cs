using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class StageConfig_RequiredDesignConcepts:DatabaseModel {

        public int RequiredDesignConceptTagId { get; set; }

        [ForeignKey("RequiredDesignConceptTagId ")]
        public virtual Tag RequiredDesignConcept { get; set; }

        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual StageConfig StageConfig { get; set; }

    }
}
