using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public class User: DatabaseModel
    {
        public string NetworkUsername { get; set; }

        public string DisplayName { set; get; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}
