using Microsoft.EntityFrameworkCore;
using MultiCatalogBillGenerator.Models;

namespace MultiCatalogBillGenerator.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CatalogItem> CatalogItems => Set<CatalogItem>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}