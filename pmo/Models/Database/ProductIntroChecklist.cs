using CustomValidators;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ViewModels.Helpers;

namespace dbModels {
    public class ProductIntroChecklist : StageHistoryModel { 
        public bool IsRequired { get; set; }

        public string Filename { get; set; }

        public int? ApprovedByUserId { get; set; }
        [ForeignKey("ApprovedByUserId")]
        public virtual User ApprovedByUser { get; set; }
        
        public DateTime ApprovedByDate { get; set; }
    }
}