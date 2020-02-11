using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public enum ProjectState {
        Go = 1,
        OnHold = 2,
        Closed = 3
    }

    public class ProjectStateHistory : DatabaseModel {
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [Required]
        public ProjectState ProjectState { get; set; }
    }
}
