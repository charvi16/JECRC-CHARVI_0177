using System;
using System.Collections.Generic;
using System.Linq;

class CustomerCategory
{
    public string CategoryName { get; private set; }

    public HashSet<string> Customers { get; private set; }

    public CustomerCategory(string categoryName, string customerData)
    {
        CategoryName = categoryName;

        Customers = new HashSet<string>(
            customerData.Split(',',
            StringSplitOptions.RemoveEmptyEntries)
        );
    }
}

class PreferenceAnalyzer
{
    private CustomerCategory electronics;
    private CustomerCategory clothing;
    private CustomerCategory books;

    public PreferenceAnalyzer(
        CustomerCategory electronics,
        CustomerCategory clothing,
        CustomerCategory books)
    {
        this.electronics = electronics;
        this.clothing = clothing;
        this.books = books;
    }

    // 1. UNION
    public HashSet<string> GetAnyCategoryCustomers()
    {
        HashSet<string> result =
            new HashSet<string>(electronics.Customers);

        result.UnionWith(clothing.Customers);
        result.UnionWith(books.Customers);

        return result;
    }

    // 2. INTERSECTION
    public HashSet<string> GetAllCategoryCustomers()
    {
        HashSet<string> result =
            new HashSet<string>(electronics.Customers);

        result.IntersectWith(clothing.Customers);
        result.IntersectWith(books.Customers);

        return result;
    }

    // 3. ONLY ELECTRONICS
    public HashSet<string> GetOnlyElectronicsCustomers()
    {
        HashSet<string> result =
            new HashSet<string>(electronics.Customers);

        result.ExceptWith(clothing.Customers);
        result.ExceptWith(books.Customers);

        return result;
    }

    // 4. ELECTRONICS AND BOOKS BUT NOT CLOTHING
    public HashSet<string> GetElectronicsAndBooksNotClothing()
    {
        HashSet<string> result =
            new HashSet<string>(electronics.Customers);

        result.IntersectWith(books.Customers);

        result.ExceptWith(clothing.Customers);

        return result;
    }

    public void Display(string title, HashSet<string> customers)
    {
        Console.WriteLine(title);

        Console.WriteLine(
            string.Join(", ", customers.OrderBy(x => x))
        );

        Console.WriteLine(
            $"Total: {customers.Count} customers\n"
        );
    }
}

class Program
{
    static void Main()
    {
        string electronicsInput =
            Console.ReadLine().Split(':')[1].Trim();

        string clothingInput =
            Console.ReadLine().Split(':')[1].Trim();

        string booksInput =
            Console.ReadLine().Split(':')[1].Trim();

        CustomerCategory electronics =
            new CustomerCategory(
                "Electronics",
                electronicsInput
            );

        CustomerCategory clothing =
            new CustomerCategory(
                "Clothing",
                clothingInput
            );

        CustomerCategory books =
            new CustomerCategory(
                "Books",
                booksInput
            );

        PreferenceAnalyzer analyzer =
            new PreferenceAnalyzer(
                electronics,
                clothing,
                books
            );

        Console.WriteLine(
            "--- Customer Preference Analysis ---\n"
        );

        analyzer.Display(
            "1. Customers in ANY category (Union):",
            analyzer.GetAnyCategoryCustomers()
        );

        analyzer.Display(
            "2. Customers in ALL categories (Intersection):",
            analyzer.GetAllCategoryCustomers()
        );

        analyzer.Display(
            "3. Customers ONLY in Electronics (Difference):",
            analyzer.GetOnlyElectronicsCustomers()
        );

        analyzer.Display(
            "4. Customers in Electronics AND Books but NOT Clothing:",
            analyzer.GetElectronicsAndBooksNotClothing()
        );
    }
}