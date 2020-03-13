using System;

namespace dbModels {
    public class ProductIntroChecklist : StageHistoryModel { 
        public bool IsRequired { get; set; }

        public string Filename { get; set; }
        
        public string ApprovedBy { get; set; }
        
        public DateTime ApprovedByDate { get; set; }
    }
}