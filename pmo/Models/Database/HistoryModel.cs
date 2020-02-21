using System;

namespace dbModels {
    public class HistoryModel : DatabaseModel {
        public int Version { get; set; }

        public DateTime LastModified { get; set; }

    }
}
