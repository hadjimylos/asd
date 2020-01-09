using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class Project_User : DatabaseModel { 
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }


    }
}
