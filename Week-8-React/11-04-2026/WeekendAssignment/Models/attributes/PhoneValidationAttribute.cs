using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AirbnbBackend.Models.Attributes
{
    public class PhoneValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Phone number is required.");

            string phone = value.ToString() ?? string.Empty;

            var regex = new Regex(@"^\+?[0-9]{10}$");

            if (!regex.IsMatch(phone))
                return new ValidationResult("Phone number must contain 10 digits and may start with '+'.");

            return ValidationResult.Success;
        }
    }
}