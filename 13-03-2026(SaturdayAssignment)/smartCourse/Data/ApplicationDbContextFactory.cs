using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SmartCourseApi.Data;

public class ApplicationDbContextFactory 
    : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseMySql(
            "server=localhost;port=3306;database=SmartCourseDB;user=root;password=root",
            ServerVersion.AutoDetect("server=localhost;port=3306;database=SmartCourseDB;user=root;password=root")
        );

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}