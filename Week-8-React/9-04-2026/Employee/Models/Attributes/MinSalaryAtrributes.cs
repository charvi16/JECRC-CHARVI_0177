using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.models.attributes
{
    public class MinimumSalaryAttribute : ValidationAttribute
    {
        private readonly decimal _minSalary;

        public MinimumSalaryAttribute(decimal minSalary)
        {
            _minSalary = minSalary;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is decimal salary && salary >= _minSalary)
                return ValidationResult.Success;

            return new ValidationResult($"Salary must be at least {_minSalary}");
        }
    }
}