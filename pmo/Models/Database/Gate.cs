namespace dbModels {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum GateDecisionType {
        // pending
        Open = 0, 
        PendingDecision = 1,
        
        // decisons
        Go = 2,
        OnHold = 3,
        Closed = 4
    }

    public class Gate : DatabaseModel
    {
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public int StageConfigId { get; set; }

        [ForeignKey("StageConfigId")]
        public virtual StageConfig StageConfig { get; set; }

        public DateTime ActualReviewDate { get; set; }

        public GateDecisionType Decision { get; set; }

        public string Comments { get; set; }

        public virtual List<GateKeeper> GateKeepers { get; set; }

        public virtual List<GateFile> GateFiles { get; set; }
    }
}