using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class CustomerDesignApproval: StageHistoryModel {
       
        public string SentForApprovalBy { get; set; }

        public DateTime DateSentForApprove { get; set; }
        
        public string ApprovedBy { get; set; }
        
        public DateTime ApprovedDate { get; set; }
    }
}