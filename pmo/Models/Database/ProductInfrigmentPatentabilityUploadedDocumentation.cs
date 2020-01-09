using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels
{
    public class ProductInfrigmentPatentabilityUploadedDocumentation : UploadedDocumentation
    {
        public int ProductInfrigmentPatentabilityId { get; set; }

        [ForeignKey("ProductInfrigmentPatentabilityId")]
        public virtual ProductInfrigmentPatentability ProductInfrigmentPatentability { get; set; }
    }
}
