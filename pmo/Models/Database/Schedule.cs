using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class Schedule:HistoryModel {
        public int StageId { get; set; }
        
        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
        
        public int TagId { get; set; }
        
        [ForeignKey("TagId")]
        public virtual Tag SchduleType { get; set; }
        
        public DateTime Date { get; set; }
    }
}
