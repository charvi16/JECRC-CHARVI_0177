using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystem.Models;

public class CartViewModel
{
    public List<CartItem> Items { get; set; } = new();
    public decimal Subtotal => Items.Sum(i => i.LineTotal);
    public decimal DeliveryFee => Items.Count == 0 ? 0 : 49;
    public decimal Tax => Math.Round(Subtotal * 0.05m, 2);
    public decimal Total => Subtotal + DeliveryFee + Tax;
}

public class CheckoutViewModel : CartViewModel
{
    [Required, StringLength(120)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, StringLength(30)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required, StringLength(400)]
    public string DeliveryAddress { get; set; } = string.Empty;

    [StringLength(400)]
    public string? Notes { get; set; }
}
