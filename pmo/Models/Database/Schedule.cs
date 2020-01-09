using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class Schedule:DatabaseModel {
        public int ProjectId { get; set; }
        
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        
        public int TagId { get; set; }
        
        [ForeignKey("TagId")]
        public virtual Tag SchduleType { get; set; }
        
        public DateTime Date { get; set; }
    }
}
