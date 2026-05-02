using Microsoft.EntityFrameworkCore;
using ems_api.Models;

namespace ems_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Employee> Employees { get; set; }
    }
}