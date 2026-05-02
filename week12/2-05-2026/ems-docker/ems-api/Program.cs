using ems_api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Get connection string once
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// FIX: Do NOT use AutoDetect (it tries to connect immediately)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 45)) // match your MySQL version
    ));

var app = builder.Build();

// Ensure DB is created (with retry to handle container startup timing)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    int retries = 5;
    while (retries > 0)
    {
        try
        {
            db.Database.EnsureCreated();
            break;
        }
        catch
        {
            retries--;
            Thread.Sleep(5000); // wait 5 sec before retry
        }
    }
}

app.UseRouting();
app.MapControllers();

app.Run();