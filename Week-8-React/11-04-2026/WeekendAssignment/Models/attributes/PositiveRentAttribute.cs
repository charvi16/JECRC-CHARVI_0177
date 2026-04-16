using System.ComponentModel.DataAnnotations;

namespace AirbnbBackend.Models.Attributes
{
    public class PositiveRentAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Rent is required.");

            if (value is decimal rent)
            {
                if (rent <= 0)
                    return new ValidationResult("Rent must be greater than 0.");

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid rent value.");
        }
    }
}