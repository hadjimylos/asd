using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class DatabaseModel {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public string ModifiedByUser { get; set; }
    }
}
