using System;

namespace dbModels {
    public class ProductIntroChecklist : DatabaseModel { 
        public bool IsMarketingRequired { get; set; }

        public string Filename { get; set; }
        
        public string ApprovedBy { get; set; }
        
        public DateTime ApprovedByDate { get; set; }
    }
}