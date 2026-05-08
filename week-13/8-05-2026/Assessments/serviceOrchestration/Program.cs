using System;
using System.Threading.Tasks;

class AsyncService
{
    // Properties
    protected int requestCount;
    protected long lastResponseTime;

    // Virtual Async Methods
    public virtual async Task<string> FetchDataAsync(string endpoint)
    {
        await Task.Delay(2000);
        return "Base Service Data";
    }

    public virtual async Task<string> GetStatusAsync()
    {
        await Task.Delay(100);

        return $"Requests:{requestCount}";
    }
}

class WeatherService : AsyncService
{
    // Additional Properties
    private string city;
    private int temperature;

    // Constructor
    public WeatherService(string city, int temperature)
    {
        this.city = city;
        this.temperature = temperature;
    }

    // Override FetchDataAsync
    public override async Task<string> FetchDataAsync(string endpoint)
    {
        requestCount++;

        Console.WriteLine($"Weather Fetch Started,{city}");

        await Task.Delay(2000);

        string result =
            $"Weather Data Received,{city},{temperature}°C";

        Console.WriteLine(result);

        return result;
    }

    // Override GetStatusAsync
    public override async Task<string> GetStatusAsync()
    {
        await Task.Delay(100);

        string status =
            $"Weather Service Status,Requests:{requestCount}";

        Console.WriteLine(status);

        return status;
    }
}

class StockService : AsyncService
{
    // Additional Properties
    private string symbol;
    private double currentPrice;

    // Constructor
    public StockService(string symbol, double currentPrice)
    {
        this.symbol = symbol;
        this.currentPrice = currentPrice;
    }

    // Override FetchDataAsync
    public override async Task<string> FetchDataAsync(string endpoint)
    {
        requestCount++;

        Console.WriteLine($"Stock Fetch Started,{symbol}");

        await Task.Delay(2000);

        string result =
            $"Stock Price Update,{symbol},${currentPrice}";

        Console.WriteLine(result);

        return result;
    }

    // Override GetStatusAsync
    public override async Task<string> GetStatusAsync()
    {
        await Task.Delay(100);

        string status =
            $"Stock Service Status,Requests:{requestCount}";

        Console.WriteLine(status);

        return status;
    }
}

class Program
{
    static async Task Main()
    {
        // Input
        string serviceType = Console.ReadLine().Trim();
        string identifier = Console.ReadLine().Trim();
        string command = Console.ReadLine().Trim();

        AsyncService service;

        // Create object based on type
        if (serviceType.Equals("Weather", StringComparison.OrdinalIgnoreCase))
        {
            // Example temperature
            service = new WeatherService(identifier, 22);
        }
        else
        {
            // Example stock price
            service = new StockService(identifier, 154.75);
        }

        // Execute command
        if (command.Equals("FetchDataAsync", StringComparison.OrdinalIgnoreCase))
        {
            await service.FetchDataAsync(identifier);
        }
        else if (command.Equals("GetStatusAsync", StringComparison.OrdinalIgnoreCase))
        {
            await service.GetStatusAsync();
        }
    }
}