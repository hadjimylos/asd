using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public class GateFile:DatabaseModel
    {
        public int GateNumber { get; set; }

        [ForeignKey("GateId")]
        public virtual Gate Gate { get; set; }

        // form fields
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
