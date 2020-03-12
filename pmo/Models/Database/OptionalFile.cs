namespace dbModels {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ViewModels.Helpers;

    public class OptionalFile : DatabaseModel {

        [Required(ErrorMessage = ErrorMessages.Required)]
        public int FileTagId { get; set; }

        [ForeignKey("FileTagId")]
        public virtual Tag FileTag { get; set; }

        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
        // form fields
        public string Url { get; set; }
        public string Description { get; set; }




    }
}
