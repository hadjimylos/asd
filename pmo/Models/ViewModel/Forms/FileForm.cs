namespace forms {
    using Microsoft.AspNetCore.Http;

    public class FileForm {
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public int TagId { get; set; }
        public string TagDescription { get; set; }
    }
}
