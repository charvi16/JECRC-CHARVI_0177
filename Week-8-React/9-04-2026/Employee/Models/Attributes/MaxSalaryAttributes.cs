using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.models.attributes
{
    public class MaximumSalaryAttribute : ValidationAttribute
    {
        private readonly decimal _maxSalary;

        public MaximumSalaryAttribute(decimal maxSalary)
        {
            _maxSalary = maxSalary;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is decimal salary && salary <= _maxSalary)
                return ValidationResult.Success;

            return new ValidationResult($"Salary must not exceed {_maxSalary}");
        }
    }
}