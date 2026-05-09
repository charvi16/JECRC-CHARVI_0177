using FoodOrderingSystem.Data;
using FoodOrderingSystem.Extensions;
using FoodOrderingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Controllers;

public class MenuController : Controller
{
    private const string CartKey = "Cart";
    private readonly ApplicationDbContext _context;

    public MenuController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? search, int? categoryId)
    {
        var query = _context.FoodItems.Include(f => f.Category).Where(f => f.IsAvailable);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch = search.ToLower();
            query = query.Where(f => f.Name.ToLower().Contains(normalizedSearch) || f.Description.ToLower().Contains(normalizedSearch));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(f => f.CategoryId == categoryId.Value);
        }

        ViewBag.Search = search;
        ViewBag.CategoryId = categoryId;
        ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();

        return View(await query.OrderBy(f => f.Category!.Name).ThenBy(f => f.Name).ToListAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int id)
    {
        var food = await _context.FoodItems.FindAsync(id);
        if (food is null || !food.IsAvailable)
        {
            return NotFound();
        }

        var cart = HttpContext.Session.GetObject<List<CartItem>>(CartKey) ?? new List<CartItem>();
        var item = cart.FirstOrDefault(i => i.FoodItemId == id);
        if (item is null)
        {
            cart.Add(new CartItem { FoodItemId = food.Id, Name = food.Name, ImageUrl = food.ImageUrl, UnitPrice = food.Price, Quantity = 1 });
        }
        else
        {
            item.Quantity++;
        }

        HttpContext.Session.SetObject(CartKey, cart);
        TempData["Message"] = $"{food.Name} added to cart.";
        return RedirectToAction(nameof(Index));
    }
}
