using System.ComponentModel.DataAnnotations;
using EmployeePortal.Models.Attributes;

namespace EmployeePortal.Models.Dto
{
    public class CreateEmployeeDto
    {
        [Required]
        public string Name { get; set; }

        [MinimumSalary(10000)]
        [MaximumSalary(100000)]
        public decimal Salary { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone must be 10 digits")]
        public string Phone { get; set; }

        public AddressDto Address { get; set; }
    }
}