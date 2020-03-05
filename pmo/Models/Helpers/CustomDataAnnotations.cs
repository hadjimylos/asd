namespace CustomValidators {
    using System;
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Helpers;

    public class UserExistsActiveDirectoryAttribute : ValidationAttribute {
        private readonly string _errorMessage = null;

        public UserExistsActiveDirectoryAttribute(string errorMessage) {
            _errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var networkUsername = (string)value;

            // only check for this specific error (e.g. "required" is another data annotation)
            if (string.IsNullOrEmpty(networkUsername))
                return ValidationResult.Success;

            var user = ActiveDirectoryHelper.GetUser(networkUsername);
            if (user == null)
                return new ValidationResult(_errorMessage);

            return ValidationResult.Success;
        }
    }

    public class MinValue : ValidationAttribute {
        private readonly long? _minValue;
        private readonly DateTime? _minDate;

        public MinValue(long minValue) {
            _minValue = minValue - 1;
        }

        public MinValue(string minValue) {
            var val = DateTime.Parse(minValue);
            _minDate = val.AddDays(-1);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var success = ValidationResult.Success;
            var errorType = _minValue?.ToString() ?? _minDate?.ToString("MM/dd/yyyy");
            string error = this.ErrorMessage ?? $"Please insert a value greater than {errorType}.";
            var fail = new ValidationResult(error);

            return Type.GetTypeCode(value.GetType()) switch {
                TypeCode.Int16 => (short)value > _minValue ? success : fail,
                TypeCode.Int32 => (int)value > _minValue ? success : fail,
                TypeCode.Int64 => (long)value > _minValue ? success : fail,
                TypeCode.Decimal => (decimal)value > _minValue ? success : fail,
                TypeCode.UInt16 => (ushort)value > _minValue ? success : fail,
                TypeCode.UInt32 => (uint)value > _minValue ? success : fail,
                TypeCode.DateTime => (DateTime)value > _minDate ? success : fail,
                _ => fail, // unsupported type
            };
        }
    }
}
