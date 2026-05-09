using FoodOrderingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class FoodItemsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FoodItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? search)
    {
        var query = _context.FoodItems.Include(f => f.Category).Where(f => f.IsAvailable);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch = search.ToLower();
            query = query.Where(f => f.Name.ToLower().Contains(normalizedSearch) || f.Description.ToLower().Contains(normalizedSearch));
        }

        var foods = await query.Select(f => new
        {
            f.Id,
            f.Name,
            f.Description,
            f.Price,
            f.ImageUrl,
            Category = f.Category!.Name,
            f.IsVegetarian,
            f.PreparationMinutes
        }).ToListAsync();

        return Ok(foods);
    }
}
