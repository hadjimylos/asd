using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class KeyCharacteristic : DatabaseModel {
        public string ItemNumber { get; set; }
        
        public string Requirement { get; set; }
        
        public int RequirementSourceId{get;set;}
        
        [ForeignKey("RequirementSourceId")]
        public virtual Tag RequirementSource { get; set; }
        
        public string MeasuredValue { get; set; }
        
        public string ExpectedOutcomeRisk { get; set; }
    }
}