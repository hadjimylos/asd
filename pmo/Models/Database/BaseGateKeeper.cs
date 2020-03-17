namespace dbModels {
    using CustomValidators;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ViewModels.Helpers;

    public class BaseGateKeeper : DatabaseModel {

        [Required(ErrorMessage = ErrorMessages.Required)]
        [MinValue(1)]
        public int UserId{ get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int GateId { get; set; }

        [ForeignKey("GateId")]
        public virtual Gate Gate { get; set; }

    }
}
