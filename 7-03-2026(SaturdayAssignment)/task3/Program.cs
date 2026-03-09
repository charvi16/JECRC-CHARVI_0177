using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}

public class Solution
{
    public static void Main()
    {
        List<Product> products = new List<Product>()
        {
            new Product { ProductId = 201, Name = "Laptop", Price = 60000, Quantity = 5 },
            new Product { ProductId = 202, Name = "Mouse", Price = 800, Quantity = 25 },
            new Product { ProductId = 203, Name = "Keyboard", Price = 1500, Quantity = 8 },
            new Product { ProductId = 204, Name = "Monitor", Price = 12000, Quantity = 12 }
        };

        var lowStock = products.Where(p => p.Quantity < 10);

        Console.WriteLine("Low Stock Products:");
        lowStock.ToList().ForEach(p => Console.WriteLine(p.Name));

        var sortedProducts = products.OrderBy(p => p.Price);

        Console.WriteLine("\nProducts Sorted by Price:");
        sortedProducts.ToList().ForEach(p => Console.WriteLine($"{p.Name} - {p.Price}"));

        var totalValue = products.Sum(p => p.Price * p.Quantity);

        Console.WriteLine("\nTotal Inventory Value:");
        Console.WriteLine("Rs " + totalValue);
        Console.WriteLine();
    }
}