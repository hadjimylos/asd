namespace dbModels {
    public enum RampResourcePlanType { Url = 1, Description = 2 }
    
    public class RampResourcePlan:DatabaseModel {
        public string Value { get; set; }
       
        public RampResourcePlanType Type { get; set; }
    }
}