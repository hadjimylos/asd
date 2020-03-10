namespace dbModels {
    using System.ComponentModel.DataAnnotations;

    public class BaseGateKeeperConfig : DatabaseModel {
        [Required]
        public string Keeper { get; set; }
    }
}
