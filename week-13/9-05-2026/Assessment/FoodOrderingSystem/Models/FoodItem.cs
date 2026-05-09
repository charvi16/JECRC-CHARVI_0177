using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingSystem.Models;

public class FoodItem
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 10000)]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [StringLength(300)]
    public string? ImageUrl { get; set; }

    public bool IsAvailable { get; set; } = true;
    public bool IsVegetarian { get; set; }

    [Range(1, 180)]
    public int PreparationMinutes { get; set; } = 20;

    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
