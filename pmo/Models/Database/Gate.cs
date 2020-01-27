using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public enum GateDecisionType
    {
        [Description("Go")]
        go = 1,
        [Description("On-Hold")]
        on_hold = 2,
        [Description("Close")]
        close = 3
    }

    public class Gate : DatabaseModel
    {

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public virtual List<GateApprovers> GateApprover { get; set; }

        public DateTime ActualReviewDate { get; set; }

        public GateDecisionType Decision { get; set; }

        public virtual List<GateUploadedDocumentation> GateUploadedDocumentation { get; set; }

        public string Comments { get; set; }
    }
}