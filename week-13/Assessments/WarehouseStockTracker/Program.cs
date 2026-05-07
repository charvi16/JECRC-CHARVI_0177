using System;
using System.Collections.Generic;

class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int productId)
        : base($"Product {productId} does not exist.")
    {
    }
}

class InsufficientStockException : Exception
{
    public InsufficientStockException(int productId, int available, int requested)
        : base($"Cannot remove {requested} units from Product {productId}. Available stock: {available}.")
    {
    }
}

class InvalidQuantityException : Exception
{
    public InvalidQuantityException(int qty)
        : base($"Invalid quantity: {qty}. Quantity must be greater than 0.")
    {
    }
}

class Product
{
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }

    public Product(int productId, int quantity)
    {
        if (quantity <= 0)
            throw new InvalidQuantityException(quantity);

        ProductId = productId;
        Quantity = quantity;
    }

    public void AddStock(int qty)
    {
        if (qty <= 0)
            throw new InvalidQuantityException(qty);

        Quantity += qty;
    }

    public void RemoveStock(int qty)
    {
        if (qty <= 0)
            throw new InvalidQuantityException(qty);

        if (qty > Quantity)
        {
            throw new InsufficientStockException(
                ProductId,
                Quantity,
                qty
            );
        }

        Quantity -= qty;
    }
}

class Warehouse
{
    private Dictionary<int, Product> inventory =
        new Dictionary<int, Product>();

    public void Add(int productId, int qty)
    {
        if (inventory.ContainsKey(productId))
        {
            inventory[productId].AddStock(qty);
        }
        else
        {
            inventory[productId] = new Product(productId, qty);
        }
    }

    public void Remove(int productId, int qty)
    {
        if (!inventory.ContainsKey(productId))
        {
            throw new ProductNotFoundException(productId);
        }

        inventory[productId].RemoveStock(qty);
    }

    public void Check(int productId)
    {
        if (!inventory.ContainsKey(productId))
        {
            throw new ProductNotFoundException(productId);
        }

        Console.WriteLine(
            $"Product {productId}: {inventory[productId].Quantity} units"
        );
    }
}

class Program
{
    static void Main()
    {
        Warehouse warehouse = new Warehouse();

        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            try
            {
                string input = Console.ReadLine();
                string[] parts = input.Split(' ');

                string command = parts[0];

                switch (command)
                {
                    case "ADD":
                        warehouse.Add(
                            int.Parse(parts[1]),
                            int.Parse(parts[2])
                        );
                        break;

                    case "REMOVE":
                        warehouse.Remove(
                            int.Parse(parts[1]),
                            int.Parse(parts[2])
                        );
                        break;

                    case "CHECK":
                        warehouse.Check(
                            int.Parse(parts[1])
                        );
                        break;
                }
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InsufficientStockException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
        }
    }
}