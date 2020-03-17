using CustomValidators;
using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class GateKeeperForm : GateKeeper {
        public string Label { get; set; }

        public SelectList UserDropDown { set; get; }

    }
}
