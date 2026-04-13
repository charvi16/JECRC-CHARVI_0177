using AirbnbBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirbnbBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties => Set<Property>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(p => p.Location)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.Rent)
                    .HasPrecision(18, 2);

                entity.Property(p => p.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.NearbySites)
                    .HasMaxLength(500);

                entity.Property(p => p.Description)
                    .HasMaxLength(1000);
            });
        }
    }
}