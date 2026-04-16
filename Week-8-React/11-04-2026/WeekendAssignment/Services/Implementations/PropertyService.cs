using AirbnbBackend.Data;
using AirbnbBackend.Mappings;
using AirbnbBackend.Models.DTOs;
using AirbnbBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirbnbBackend.Services.Implementations
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext _context;

        public PropertyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PropertyResponseDto>> GetAllAsync()
        {
            var properties = await _context.Properties
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return properties.Select(p => p.ToResponseDto());
        }

        public async Task<PropertyResponseDto?> GetByIdAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property == null)
                return null;

            return property.ToResponseDto();
        }

        public async Task<PropertyResponseDto> CreateAsync(CreatePropertyDto dto)
        {
            var property = dto.ToEntity();

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return property.ToResponseDto();
        }

        public async Task<bool> UpdateAsync(int id, UpdatePropertyDto dto)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property == null)
                return false;

            dto.MapToExistingEntity(property);

            _context.Properties.Update(property);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property == null)
                return false;

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}