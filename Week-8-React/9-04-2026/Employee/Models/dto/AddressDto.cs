using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.Models.Dto
{
    public class AddressDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ? City{get; set;}

        [Required]
        [StringLength(50, MinimumLength = 3)]

        public string ? Street{get; set;}

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string PostalCode {get; set;}

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ? Landmark {get; set;}

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string ? State {get; set;}

    }
}
