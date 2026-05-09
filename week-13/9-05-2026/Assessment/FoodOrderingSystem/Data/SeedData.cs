using FoodOrderingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Data;

public static class SeedData
{
    public const string AdminRole = "Admin";
    public const string CustomerRole = "Customer";

    public static async Task InitializeAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        foreach (var role in new[] { AdminRole, CustomerRole })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        const string adminEmail = "admin@foodhub.local";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true, PhoneNumber = "9999999999" };
            await userManager.CreateAsync(admin, "Admin@12345");
        }

        if (!await userManager.IsInRoleAsync(admin, AdminRole))
        {
            await userManager.AddToRoleAsync(admin, AdminRole);
        }

        if (await context.Categories.AnyAsync())
        {
            return;
        }

        var categories = new[]
        {
            new Category { Name = "Pizza", Description = "Hand-tossed pizzas with fresh toppings." },
            new Category { Name = "Biryani", Description = "Slow-cooked rice bowls with layered spices." },
            new Category { Name = "Burgers", Description = "Stacked burgers, fries, and quick bites." },
            new Category { Name = "Desserts", Description = "Sweet finishes for every order." }
        };

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();

        context.FoodItems.AddRange(
            new FoodItem { Name = "Margherita Pizza", CategoryId = categories[0].Id, Price = 249, PreparationMinutes = 18, IsVegetarian = true, ImageUrl = "/images/hero-food.png", Description = "Classic tomato, basil, and mozzarella on a crisp crust." },
            new FoodItem { Name = "Paneer Tikka Pizza", CategoryId = categories[0].Id, Price = 329, PreparationMinutes = 22, IsVegetarian = true, ImageUrl = "/images/hero-food.png", Description = "Smoky paneer, peppers, onions, and tikka sauce." },
            new FoodItem { Name = "Hyderabadi Biryani", CategoryId = categories[1].Id, Price = 299, PreparationMinutes = 25, ImageUrl = "/images/hero-food.png", Description = "Fragrant basmati rice with rich masala and raita." },
            new FoodItem { Name = "Veg Dum Biryani", CategoryId = categories[1].Id, Price = 239, PreparationMinutes = 24, IsVegetarian = true, ImageUrl = "/images/hero-food.png", Description = "Layered vegetables, saffron rice, and aromatic spices." },
            new FoodItem { Name = "Crispy Classic Burger", CategoryId = categories[2].Id, Price = 179, PreparationMinutes = 15, ImageUrl = "/images/hero-food.png", Description = "Crunchy patty, lettuce, cheese, and house sauce." },
            new FoodItem { Name = "Chocolate Lava Cake", CategoryId = categories[3].Id, Price = 129, PreparationMinutes = 10, IsVegetarian = true, ImageUrl = "/images/hero-food.png", Description = "Warm chocolate cake with a molten center." }
        );
        await context.SaveChangesAsync();
    }
}
