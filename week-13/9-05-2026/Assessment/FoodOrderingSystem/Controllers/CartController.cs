using FoodOrderingSystem.Data;
using FoodOrderingSystem.Extensions;
using FoodOrderingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Controllers;

public class CartController : Controller
{
    private const string CartKey = "Cart";
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View(new CartViewModel { Items = GetCart() });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, int quantity)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(i => i.FoodItemId == id);
        if (item is not null)
        {
            if (quantity <= 0)
            {
                cart.Remove(item);
            }
            else
            {
                item.Quantity = Math.Min(quantity, 99);
            }
        }

        SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var cart = GetCart();
        cart.RemoveAll(i => i.FoodItemId == id);
        SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public async Task<IActionResult> Checkout()
    {
        var user = await _userManager.GetUserAsync(User);
        return View(new CheckoutViewModel
        {
            Items = GetCart(),
            Email = user?.Email ?? string.Empty,
            PhoneNumber = user?.PhoneNumber ?? string.Empty
        });
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        model.Items = GetCart();
        if (model.Items.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Your cart is empty.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = _userManager.GetUserId(User) ?? string.Empty;
        var order = new Order
        {
            UserId = userId,
            CustomerName = model.CustomerName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            DeliveryAddress = model.DeliveryAddress,
            Notes = model.Notes,
            Subtotal = model.Subtotal,
            DeliveryFee = model.DeliveryFee,
            Tax = model.Tax,
            Total = model.Total,
            Items = model.Items.Select(i => new OrderItem
            {
                FoodItemId = i.FoodItemId,
                FoodName = i.Name,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                LineTotal = i.LineTotal
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        SaveCart(new List<CartItem>());

        return RedirectToAction("Invoice", "Orders", new { id = order.Id });
    }

    private List<CartItem> GetCart() => HttpContext.Session.GetObject<List<CartItem>>(CartKey) ?? new List<CartItem>();
    private void SaveCart(List<CartItem> cart) => HttpContext.Session.SetObject(CartKey, cart);
}
