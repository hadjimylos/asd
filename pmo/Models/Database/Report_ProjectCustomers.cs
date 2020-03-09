using bModels.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dbModels.Report
{
    public class Report_ProjectCustomers : Report_DatabaseModel
    {
        public int CustomersTagId { get; set; }
        public int ProjectId { get; set; }


        [ForeignKey("CustomersTagId")]
        public virtual Tag Customers { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Report_Project Report_Project { get; set; }
    }
}
