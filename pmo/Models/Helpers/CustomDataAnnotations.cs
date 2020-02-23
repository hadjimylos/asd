namespace CustomValidators {
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
}
