using CustomValidators;
using dbModels;
using System.ComponentModel.DataAnnotations;
using ViewModels.Helpers;

namespace forms {
    public class FinancialDataForm : FinancialData {
        [Required(ErrorMessage = ErrorMessages.Required)]
        public new int Quantity { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal StdCostEstimated { get; set; }

        [Required(ErrorMessage = ErrorMessages.Required)]
        public new decimal SalesPriceEstimated { get; set; }

        public new decimal? GPACapital { get; set; }
        public new decimal? GPAExpense { get; set; }
        public new decimal? QualCosts { get; set; }
        public new decimal? OtherDevelopmentExpenses { get; set; }
    }
}
