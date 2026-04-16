using System.ComponentModel.DataAnnotations;
using AirbnbBackend.Models.Attributes;

namespace AirbnbBackend.Models.DTOs
{
    public class UpdatePropertyDto
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Location { get; set; } = string.Empty;

        [PositiveRent]
        public decimal Rent { get; set; }

        [StringLength(500)]
        public string? NearbySites { get; set; }

        [Required]
        [PhoneValidation]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(1, 50)]
        public int MaxGuests { get; set; }

        public bool IsAvailable { get; set; }
    }
}