using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class CustomerDesignApproval: HistoryModel
    {
       
        public string SentForApprovalBy { get; set; }

        public DateTime DateSentForApprove { get; set; }
        
        public string ApprovedBy { get; set; }
        
        public DateTime ApprovedDate { get; set; }

        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
    }
}