using System;

namespace dbModels {
    public class HistoryModel:DatabaseModel {
        public DateTime StartUpdateDate { get; set; }
        
        public DateTime EndUpdateDate { get; set; }
    }
}
