using System.ComponentModel.DataAnnotations.Schema;

namespace dbModels {
    public class FinancialData : DatabaseModel {
        public int BusinessCaseId { get; set; }

        [ForeignKey("BusinessCaseId")]
        public virtual BusinessCase BusinessCase { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
        public decimal StdCostEstimated { get; set; }   
        public decimal SalesCostEstimated { get; set; }

        public decimal GPACapital { get; set; }
        public decimal GPAExpense { get; set; }
        public decimal QualCosts { get; set; }
        public decimal OtherDevelopmentExpenses { get; set; }


    }
}
