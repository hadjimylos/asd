using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class BaseStageFileConfig : DatabaseModel {
        public int RequiredFileTagId { get; set; }

        [ForeignKey("RequiredFileTagId")]
        public virtual Tag RequiredFile { get; set; }

        public bool IsLocation { get; set; }
    }
}
