namespace ViewModels {
    using Microsoft.AspNetCore.Http;

    public class SharepointFile {
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public int TagId { get; set; }
        public string TagDescription { get; set; }
    }
}
