using System;
using System.Collections.Generic;
using System.Linq;

class SalesRecord
{
    public string ProductId { get; set; }

    public string Region { get; set; }

    public double Amount { get; set; }

    public SalesRecord(
        string productId,
        string region,
        double amount)
    {
        ProductId = productId;
        Region = region;
        Amount = amount;
    }
}

class ProductSales
{
    public string ProductId { get; private set; }

    public Dictionary<string, double> RegionSales
        = new Dictionary<string, double>();

    public ProductSales(string productId)
    {
        ProductId = productId;
    }

    public void AddSale(string region, double amount)
    {
        if (RegionSales.ContainsKey(region))
        {
            RegionSales[region] += amount;
        }
        else
        {
            RegionSales[region] = amount;
        }
    }

    public double GetTotalSales()
    {
        return RegionSales.Values.Sum();
    }

    public double GetAverageSales()
    {
        return RegionSales.Values.Average();
    }

    public double GetMinSales()
    {
        return RegionSales.Values.Min();
    }

    public double GetMaxSales()
    {
        return RegionSales.Values.Max();
    }
}

class SalesAggregator
{
    private Dictionary<string, ProductSales> products =
        new Dictionary<string, ProductSales>();

    private List<SalesRecord> records =
        new List<SalesRecord>();

    public void AddRecord(SalesRecord record)
    {
        records.Add(record);

        if (!products.ContainsKey(record.ProductId))
        {
            products[record.ProductId] =
                new ProductSales(record.ProductId);
        }

        products[record.ProductId]
            .AddSale(record.Region, record.Amount);
    }

    // 1. Product-wise report
    public void DisplaySalesReport()
    {
        Console.WriteLine(
            "--- Sales Report by Product and Region ---\n"
        );

        foreach (var product in products.Values)
        {
            Console.WriteLine(
                $"Product {product.ProductId}:"
            );

            foreach (var region in product.RegionSales)
            {
                Console.WriteLine(
                    $"  {region.Key}: ${region.Value}"
                );
            }

            Console.WriteLine(
                $"  Total: ${product.GetTotalSales()}, " +
                $"Average: ${product.GetAverageSales():F2}\n"
            );
        }
    }

    // 2. Best selling product by region
    public void DisplayBestSellingByRegion()
    {
        Console.WriteLine(
            "Best Selling Product by Region:\n"
        );

        var groupedByRegion = records
            .GroupBy(r => r.Region);

        foreach (var regionGroup in groupedByRegion)
        {
            var best = regionGroup
                .OrderByDescending(r => r.Amount)
                .First();

            Console.WriteLine(
                $"{regionGroup.Key}: " +
                $"{best.ProductId} (${best.Amount})"
            );
        }

        Console.WriteLine();
    }

    // 3. Underperforming products
    public void DisplayUnderperformingProducts(
        double threshold)
    {
        Console.WriteLine(
            $"Underperforming Products " +
            $"(< ${threshold} average):\n"
        );

        var underperformers = products.Values
            .Where(p => p.GetAverageSales() < threshold);

        foreach (var product in underperformers)
        {
            Console.WriteLine(
                $"{product.ProductId} " +
                $"(${product.GetAverageSales():F2})"
            );
        }
    }
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        SalesAggregator aggregator =
            new SalesAggregator();

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries
            );

            string productId = input[0];
            string region = input[1];
            double amount = double.Parse(input[2]);

            aggregator.AddRecord(
                new SalesRecord(
                    productId,
                    region,
                    amount
                )
            );
        }

        double threshold = double.Parse(
            Console.ReadLine()
        );

        aggregator.DisplaySalesReport();

        aggregator.DisplayBestSellingByRegion();

        aggregator.DisplayUnderperformingProducts(
            threshold
        );
    }
}