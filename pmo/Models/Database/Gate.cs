using System;
using System.Collections.Generic;

namespace dbModels {
    public enum GateDecisionType { go=1 , on_hold=2, close=3 }

    public class Gate: DatabaseModel {
        public virtual List<GateApprovers> GateApprover { get; set; }
        
        public DateTime ActualReviewDate { get; set; }
        
        public GateDecisionType Decision { get; set; }
        
        public virtual List<UploadedDocumentation> UploadedDocumentation { get; set; }
        
        public string Comments { get; set; }
    }
}