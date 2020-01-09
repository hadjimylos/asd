namespace dbModels {
    public class QualificationTesting:DatabaseModel {
        public bool IsQualificationRequired { get; set; }

        public string Description { get; set; }

        public string AttachmentUrl { get; set; }
    }
}