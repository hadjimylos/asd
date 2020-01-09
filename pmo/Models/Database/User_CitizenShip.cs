using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dbModels
{
    public class User_CitizenShip:DatabaseModel{
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Citizenships { get; set; }


    }
}
