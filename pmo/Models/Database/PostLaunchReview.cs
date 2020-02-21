using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public class PostLaunchReview : StageHistoryModel {
        public string DoneWell { get; set; }
        public string DonePoorly { get; set; }
        public string Bottlenecks { get; set; }
        public string LessonsLearned { get; set; }
        public string ActualVSExpected { get; set; }
        public string Commercial { get; set; }

    }
}
