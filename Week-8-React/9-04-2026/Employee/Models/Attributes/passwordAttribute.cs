using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.models.attributes
{
    public class passwordAttribute : ValidationAttribute
    {
        private readonly int minLength;

        public passwordAttribute(int minLength = 8) 
        {
            this.minLength = minLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Password is required.");

            if (password.Length < _minLength)
                return new ValidationResult($"Password must be at least {_minLength} characters long.");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult("Password must contain at least one uppercase letter.");

            if (!Regex.IsMatch(password, @"[a-z]"))
                return new ValidationResult("Password must contain at least one lowercase letter.");

             if (!Regex.IsMatch(password, @"[0-9]"))
                return new ValidationResult("Password must contain at least one digit.");

             if (!Regex.IsMatch(password, @"[\W_]"))
                return new ValidationResult("Password must contain at least one special character.");
            
            return ValidationResult.Success;
        }
    }
}