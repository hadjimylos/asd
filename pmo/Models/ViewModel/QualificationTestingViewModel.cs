using dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class QualificationTestingViewModel: QualificationTesting
    {
        public List<QualificationTestingViewModel> Versions { get; set; }
            public new bool IsQualificationRequired { get; set; }

            public new string Description { get; set; }

            public new string AttachmentUrl { get; set; }

            public new virtual Stage Stage { get; set; }
     }
}

