using MultiCatalogBillGenerator.Models;

namespace MultiCatalogBillGenerator.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.CatalogItems.Any()) return;

            db.CatalogItems.AddRange(
                new CatalogItem { Name = "Adult Ticket", CatalogType = "Entrance", DefaultPrice = 200, TaxPercentage = 5, IsCustomPriceAllowed = false },
                new CatalogItem { Name = "Child Ticket", CatalogType = "Entrance", DefaultPrice = 100, TaxPercentage = 5, IsCustomPriceAllowed = false },
                new CatalogItem { Name = "Senior Ticket", CatalogType = "Entrance", DefaultPrice = 150, TaxPercentage = 5, IsCustomPriceAllowed = false },
                new CatalogItem { Name = "VIP Ticket", CatalogType = "Entrance", DefaultPrice = 500, TaxPercentage = 12, IsCustomPriceAllowed = false },

                new CatalogItem { Name = "Donation ₹100", CatalogType = "Donation", DefaultPrice = 100, TaxPercentage = 0, IsCustomPriceAllowed = true },
                new CatalogItem { Name = "Donation ₹500", CatalogType = "Donation", DefaultPrice = 500, TaxPercentage = 0, IsCustomPriceAllowed = true },
                new CatalogItem { Name = "Donation ₹1000", CatalogType = "Donation", DefaultPrice = 1000, TaxPercentage = 0, IsCustomPriceAllowed = true },

                new CatalogItem { Name = "T-Shirt", CatalogType = "SellingPrice", DefaultPrice = 799, TaxPercentage = 12, IsCustomPriceAllowed = false },
                new CatalogItem { Name = "Coffee", CatalogType = "SellingPrice", DefaultPrice = 120, TaxPercentage = 5, IsCustomPriceAllowed = false },
                new CatalogItem { Name = "Guide Service", CatalogType = "SellingPrice", DefaultPrice = 300, TaxPercentage = 18, IsCustomPriceAllowed = true }
            );

            db.SaveChanges();
        }
    }
}