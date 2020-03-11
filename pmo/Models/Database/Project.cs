using dbModels.Report;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dbModels {
    public class Project : DatabaseModel {
        [Required]
        public string Name { get; set; }

        public virtual List<ProjectDetail> ProjectDetails { get; set; }
        
        public virtual List<Stage> StageHistory { get; set; }
        
        public virtual List<Gate> GateHistory { get; set; }

        public virtual List<Project_User> TeamMembers { get; set; }

        public virtual List<ProjectStateHistory> ProjectStateHistory { get; set; }
        
        public virtual Report_Project Report_Project { set; get; } 
    }
}
