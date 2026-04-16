using AirbnbBackend.Entities;
using AirbnbBackend.Models.DTOs;

namespace AirbnbBackend.Mappings
{
    public static class PropertyMappingExtensions
    {
        public static Property ToEntity(this CreatePropertyDto dto)
        {
            return new Property
            {
                Name = dto.Name,
                Location = dto.Location,
                Rent = dto.Rent,
                NearbySites = dto.NearbySites,
                Phone = dto.Phone,
                Email = dto.Email,
                Description = dto.Description,
                MaxGuests = dto.MaxGuests,
                IsAvailable = dto.IsAvailable,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void MapToExistingEntity(this UpdatePropertyDto dto, Property property)
        {
            property.Name = dto.Name;
            property.Location = dto.Location;
            property.Rent = dto.Rent;
            property.NearbySites = dto.NearbySites;
            property.Phone = dto.Phone;
            property.Email = dto.Email;
            property.Description = dto.Description;
            property.MaxGuests = dto.MaxGuests;
            property.IsAvailable = dto.IsAvailable;
            property.UpdatedAt = DateTime.UtcNow;
        }

        public static PropertyResponseDto ToResponseDto(this Property property)
        {
            return new PropertyResponseDto
            {
                Id = property.Id,
                Name = property.Name,
                Location = property.Location,
                Rent = property.Rent,
                NearbySites = property.NearbySites,
                Phone = property.Phone,
                Email = property.Email,
                Description = property.Description,
                MaxGuests = property.MaxGuests,
                IsAvailable = property.IsAvailable,
                CreatedAt = property.CreatedAt,
                UpdatedAt = property.UpdatedAt
            };
        }
    }
}