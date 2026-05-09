using FoodOrderingSystem.Data;
using FoodOrderingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Controllers;

[Authorize(Roles = SeedData.AdminRole)]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public AdminController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Dashboard()
    {
        ViewBag.FoodCount = await _context.FoodItems.CountAsync();
        ViewBag.PendingOrders = await _context.Orders.CountAsync(o => o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled);
        ViewBag.Revenue = await _context.Orders.SumAsync(o => (decimal?)o.Total) ?? 0;
        return View(await _context.Orders.Include(o => o.Items).OrderByDescending(o => o.OrderedAt).Take(8).ToListAsync());
    }

    public async Task<IActionResult> FoodItems()
    {
        return View(await _context.FoodItems.Include(f => f.Category).OrderBy(f => f.Name).ToListAsync());
    }

    public async Task<IActionResult> UpsertFood(int? id)
    {
        await LoadCategoriesAsync();
        if (id is null)
        {
            return View(new FoodItem { IsAvailable = true, PreparationMinutes = 20 });
        }

        var food = await _context.FoodItems.FindAsync(id.Value);
        return food is null ? NotFound() : View(food);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpsertFood(FoodItem food, IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return View(food);
        }

        if (image is not null && image.Length > 0)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);
            var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(image.FileName)}";
            await using var stream = System.IO.File.Create(Path.Combine(uploads, fileName));
            await image.CopyToAsync(stream);
            food.ImageUrl = $"/uploads/{fileName}";
        }

        if (food.Id == 0)
        {
            _context.FoodItems.Add(food);
        }
        else
        {
            _context.FoodItems.Update(food);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(FoodItems));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteFood(int id)
    {
        var food = await _context.FoodItems.FindAsync(id);
        if (food is not null)
        {
            _context.FoodItems.Remove(food);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(FoodItems));
    }

    public async Task<IActionResult> Categories()
    {
        return View(await _context.Categories.OrderBy(c => c.Name).ToListAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveCategory(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View("Categories", await _context.Categories.OrderBy(c => c.Name).ToListAsync());
        }

        if (category.Id == 0)
        {
            _context.Categories.Add(category);
        }
        else
        {
            _context.Categories.Update(category);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Categories));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is not null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Categories));
    }

    public async Task<IActionResult> Orders()
    {
        return View(await _context.Orders.Include(o => o.Items).OrderByDescending(o => o.OrderedAt).ToListAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is not null)
        {
            order.Status = status;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Orders));
    }

    private async Task LoadCategoriesAsync()
    {
        ViewBag.Categories = new SelectList(await _context.Categories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name");
    }
}
