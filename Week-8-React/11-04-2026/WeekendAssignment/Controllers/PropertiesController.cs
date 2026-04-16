using AirbnbBackend.Models.DTOs;
using AirbnbBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _propertyService.GetAllAsync();
            return Ok(properties);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await _propertyService.GetByIdAsync(id);

            if (property == null)
                return NotFound(new { message = $"Property with ID {id} not found." });

            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProperty = await _propertyService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdProperty.Id }, createdProperty);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _propertyService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound(new { message = $"Property with ID {id} not found." });

            return Ok(new { message = "Property updated successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _propertyService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = $"Property with ID {id} not found." });

            return Ok(new { message = "Property deleted successfully." });
        }
    }
}