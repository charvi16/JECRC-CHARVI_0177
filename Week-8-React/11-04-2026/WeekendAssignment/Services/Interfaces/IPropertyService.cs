using AirbnbBackend.Models.DTOs;

namespace AirbnbBackend.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyResponseDto>> GetAllAsync();
        Task<PropertyResponseDto?> GetByIdAsync(int id);
        Task<PropertyResponseDto> CreateAsync(CreatePropertyDto dto);
        Task<bool> UpdateAsync(int id, UpdatePropertyDto dto);
        Task<bool> DeleteAsync(int id);
    }
}