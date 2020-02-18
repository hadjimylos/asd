using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public enum GateDecisionType {
        // pending
        Open = 0, 
        
        // decisons
        Go = 1,
        OnHold = 2,
        Closed = 3
    }

    public class Gate : DatabaseModel
    {
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public DateTime ActualReviewDate { get; set; }

        public GateDecisionType Decision { get; set; }

        public string Comments { get; set; }

        public int GateConfigId { get; set; }

        [ForeignKey("GateConfigId")]
        public virtual GateConfig GateConfig { get; set; }

        public virtual List<GateKeeper> GateKeepers { get; set; }
    }
}