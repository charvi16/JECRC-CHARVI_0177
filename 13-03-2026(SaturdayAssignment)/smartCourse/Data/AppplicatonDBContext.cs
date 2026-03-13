using Microsoft.EntityFrameworkCore;
using SmartCourseApi.Models;

namespace SmartCourseApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Email for Users
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();


            // Prevent duplicate enrollments (same student in same course)
            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.CourseId, e.StudentId })
                .IsUnique();


            // Relationships

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);


            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);


            // Seed initial departments
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "Computer Science"
                },
                new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "Information Technology"
                },
                new Department
                {
                    DepartmentId = 3,
                    DepartmentName = "Electronics"
                }
            );
        }
    }
}