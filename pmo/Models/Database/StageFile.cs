namespace dbModels {
    using System.ComponentModel.DataAnnotations.Schema;
    public class StageFile : DatabaseModel {
        public int FileTagId { get; set; }

        [ForeignKey("FileTagId")]
        public virtual Tag FileTag { get; set; }

        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }

        // form fields
        public string Url { get; set; }

        public string Description { get; set; }

        public bool IsLocation { get; set; }

    }
}
