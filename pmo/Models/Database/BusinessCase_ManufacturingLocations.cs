using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels{
    public class BusinessCase_ManufacturingLocations: DatabaseModel {
        public int ManufacturingLocationsTagId { get; set; }

        [ForeignKey("ManufacturingLocationsTagId")]
        public virtual Tag ManufacturingLocations { get; set; }

        public int BusinessCaseId { get; set; }

        [ForeignKey("BusinessCaseId")]
        public virtual BusinessCase BusinessCase { get; set; }
    }
}