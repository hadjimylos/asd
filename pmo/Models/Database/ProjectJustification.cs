
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public  class ProjectJustification : HistoryModel
    {
       
        public int StageId { get; set; }
        
        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }

        public string CustomerMotivation { get; set; }

        public string Application { get; set; }

        public string DrawingSpecStandards { get; set; }

        public string RegulatoryRequirements { get; set; }

        public string OtherRequirements { get; set; }

        public string TechnicalFeasibility { get; set; }

        public string Scope { get; set; }

        public bool AddToInhouseTechnicalAbilities { get; set; }

        public int? ProductTagId { get; set; }

        [ForeignKey("ProductTagId")]
        public virtual Tag Product { get; set; }
        
        public string Competetion { get; set; }
        
        public string BenchmarkSamples { get; set; }
        
        public string AdvantagesWeOffer { get; set; }
        
        public string WhyOurOfferPreferred { get; set; }
        
        public bool? SingleUseProduct { get; set; }
    }
}