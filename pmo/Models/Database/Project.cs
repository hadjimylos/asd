using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dbModels {
    public class Project : DatabaseModel {
        [Required]
        public string Name { get; set; }
        
        public virtual List<ProjectDetail> ProjectDetail { get; set; }
        
        public virtual List<Stage> StageHistory { get; set; }
        
        public virtual List<Gate> GateHistory { get; set; }

        public virtual List<Project_User> TeamMember { get; set; }
    }
}
