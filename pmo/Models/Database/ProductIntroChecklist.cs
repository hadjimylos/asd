using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class ProductIntroChecklist : StageHistoryModel { 
        public bool IsMarketingRequired { get; set; }

        public string Filename { get; set; }
        
        public string ApprovedBy { get; set; }
        
        public DateTime ApprovedByDate { get; set; }
    }
}