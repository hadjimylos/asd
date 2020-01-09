using System;
using System.Collections.Generic;

namespace dbModels {
    public class CustomerDesignApproval:DatabaseModel {
        //TODO
        //public int SentForApprovalById { get; set; }
        //[ForeignKey("SentForApprovalById")]
        public string SentForApprovalBy { get; set; }

        public DateTime DateSentForApprove { get; set; }
        
        public string ApprovedBy { get; set; }
        
        public DateTime ApprovedDate { get; set; }
        
        public virtual List<UploadedDocumentation> ImportantDocumentation { get; set; }
    }
}