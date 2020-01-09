using System.Collections.Generic;

namespace dbModels {
    public class Project : DatabaseModel {
        public string Name { get; set; }
        
        public virtual List<ProjectDetail> ProjectDetailHistory { get; set; }
        
        public virtual List<Stage> StageHistory { get; set; }
        
        public virtual List<Gate> GateHistory { get; set; }

        public virtual List<Project_User> TeamMember { get; set; }
    }
}
