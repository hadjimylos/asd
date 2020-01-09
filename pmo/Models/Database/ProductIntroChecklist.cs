using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class ProductIntroChecklist : HistoryModel
    { 
        public bool IsMarketingRequired { get; set; }

        public string Filename { get; set; }
        
        public string ApprovedBy { get; set; }
        
        public DateTime ApprovedByDate { get; set; }

        public int StageId { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
    }
}