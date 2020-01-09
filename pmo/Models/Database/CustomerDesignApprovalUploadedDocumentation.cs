using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public class CustomerDesignApprovalUploadedDocumentation: UploadedDocumentation
    {
        public int CustomerDesignApprovalId { get; set; }

        [ForeignKey("CustomerDesignApprovalId")]
        public virtual CustomerDesignApproval CustomerDesignApproval { get; set; }
    }
}