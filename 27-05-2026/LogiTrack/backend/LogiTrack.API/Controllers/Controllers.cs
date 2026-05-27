using LogiTrack.Application.DTOs;
using LogiTrack.Application.Interfaces;
using LogiTrack.Core.Entities;
using LogiTrack.Core.Enums;
using LogiTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace LogiTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly IShipmentService _svc;
    public ShipmentsController(IShipmentService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? status = null)
        => Ok(await _svc.GetShipmentsAsync(page, pageSize, status));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _svc.GetShipmentByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("track/{trackingNumber}")]
    public async Task<IActionResult> Track(string trackingNumber)
    {
        var result = await _svc.TrackShipmentAsync(trackingNumber);
        return result == null ? NotFound(new { message = $"Tracking number {trackingNumber} not found" }) : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShipmentDto dto)
    {
        var result = await _svc.CreateShipmentAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest req)
        => Ok(await _svc.UpdateShipmentStatusAsync(id, req.Status));

    [HttpPost("{id:guid}/assign")]
    public async Task<IActionResult> Assign(Guid id, [FromBody] AssignRequest req)
    {
        await _svc.AssignDriverAndVehicleAsync(id, req.DriverId, req.VehicleId);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _svc.DeleteShipmentAsync(id);
        return NoContent();
    }
}

public record UpdateStatusRequest(string Status);
public record AssignRequest(Guid DriverId, Guid VehicleId);

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _svc;
    public VehiclesController(IVehicleService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllVehiclesAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _svc.GetVehicleByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("fleet-summary")]
    public async Task<IActionResult> FleetSummary() => Ok(await _svc.GetFleetStatusSummaryAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleDto dto)
    {
        var result = await _svc.CreateVehicleAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateVehicleDto dto)
        => Ok(await _svc.UpdateVehicleAsync(id, dto));

    [HttpPost("telemetry")]
    public async Task<IActionResult> Telemetry([FromBody] VehicleLocationUpdateDto dto)
    {
        await _svc.ProcessTelemetryAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _svc.DeleteVehicleAsync(id);
        return NoContent();
    }
}

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _svc;
    public AnalyticsController(IAnalyticsService svc) => _svc = svc;

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard() => Ok(await _svc.GetDashboardSummaryAsync());

    [HttpGet("delivery-trend")]
    public async Task<IActionResult> DeliveryTrend([FromQuery] int days = 30)
        => Ok(await _svc.GetDeliveryTrendAsync(days));

    [HttpGet("fuel-trend")]
    public async Task<IActionResult> FuelTrend([FromQuery] int days = 30)
        => Ok(await _svc.GetFuelConsumptionTrendAsync(days));

    [HttpGet("revenue")]
    public async Task<IActionResult> Revenue([FromQuery] int months = 12)
        => Ok(await _svc.GetRevenueByMonthAsync(months));
}

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] AppDbContext db) =>
        Ok(await db.Drivers
            .Include(d => d.Vehicle)
            .OrderBy(d => d.FirstName)
            .Select(d => new DriverDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Phone = d.Phone,
                LicenseNumber = d.LicenseNumber,
                LicenseExpiry = d.LicenseExpiry,
                Status = d.Status.ToString(),
                Rating = d.Rating,
                TotalDeliveries = d.TotalDeliveries,
                PhotoUrl = d.PhotoUrl,
                AssignedVehicle = d.Vehicle != null ? d.Vehicle.Name : null
            })
            .ToListAsync());
}

[ApiController]
[Route("api/[controller]")]
public class WarehousesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] AppDbContext db) =>
        Ok(await db.Warehouses
            .OrderBy(w => w.Name)
            .Select(w => new WarehouseDto
            {
                Id = w.Id,
                Name = w.Name,
                Address = w.Address,
                Latitude = w.Latitude,
                Longitude = w.Longitude,
                TotalCapacity = w.TotalCapacity,
                UsedCapacity = w.UsedCapacity,
                ManagerName = w.ManagerName,
                IsActive = w.IsActive
            })
            .ToListAsync());

    [HttpGet("{id:guid}/inventory")]
    public async Task<IActionResult> GetInventory(Guid id, [FromServices] AppDbContext db) =>
        Ok(await db.InventoryItems
            .Include(i => i.Warehouse)
            .Where(i => i.WarehouseId == id)
            .OrderBy(i => i.SKU)
            .Select(i => new InventoryItemDto
            {
                Id = i.Id,
                SKU = i.SKU,
                Name = i.Name,
                Category = i.Category,
                Quantity = i.Quantity,
                MinStockLevel = i.MinStockLevel,
                Location = i.Location,
                WarehouseName = i.Warehouse.Name
            })
            .ToListAsync());

    [HttpPost("{id:guid}/inventory")]
    public async Task<IActionResult> AddInventoryItem(Guid id, [FromBody] CreateInventoryItemRequest req, [FromServices] AppDbContext db)
    {
        if (!await db.Warehouses.AnyAsync(w => w.Id == id)) return NotFound();

        var item = new InventoryItem
        {
            WarehouseId = id,
            SKU = req.SKU,
            Name = req.Name,
            Category = req.Category,
            Quantity = req.Quantity,
            MinStockLevel = req.MinStockLevel,
            Location = req.Location,
            UnitWeight = req.UnitWeight,
            BarcodeData = req.BarcodeData
        };

        db.InventoryItems.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetInventory), new { id }, item);
    }

    [HttpPatch("inventory/{itemId:guid}/quantity")]
    public async Task<IActionResult> UpdateQuantity(Guid itemId, [FromBody] UpdateInventoryQuantityRequest req, [FromServices] AppDbContext db)
    {
        var item = await db.InventoryItems.FindAsync(itemId);
        if (item == null) return NotFound();
        item.Quantity = req.Quantity;
        item.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return NoContent();
    }
}

public record CreateInventoryItemRequest(
    string SKU,
    string Name,
    string Category,
    int Quantity,
    int MinStockLevel,
    string Location,
    decimal UnitWeight,
    string? BarcodeData);

public record UpdateInventoryQuantityRequest(int Quantity);

[ApiController]
[Route("api/[controller]")]
public class RoutesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] AppDbContext db) =>
        Ok(await db.Routes
            .Include(r => r.Waypoints.OrderBy(w => w.Order))
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new RouteDto
            {
                Id = r.Id,
                Name = r.Name,
                Origin = r.Origin,
                Destination = r.Destination,
                OriginLat = r.OriginLat,
                OriginLng = r.OriginLng,
                DestinationLat = r.DestinationLat,
                DestinationLng = r.DestinationLng,
                DistanceKm = r.DistanceKm,
                EstimatedDurationMinutes = r.EstimatedDurationMinutes,
                FuelCostEstimate = r.FuelCostEstimate,
                IsOptimized = r.IsOptimized,
                Waypoints = r.Waypoints.Select(w => new WaypointDto
                {
                    Order = w.Order,
                    Name = w.Name,
                    Latitude = w.Latitude,
                    Longitude = w.Longitude
                }).ToList()
            })
            .ToListAsync());

    [HttpPost("optimize")]
    public async Task<IActionResult> Optimize([FromBody] OptimizeRouteRequestDto dto, [FromServices] AppDbContext db)
    {
        var stopCount = dto.Waypoints.Count;
        var distance = Math.Max(25d, (dto.Origin.Length + dto.Destination.Length) * 37d + stopCount * 42d);
        if (dto.AvoidHighways) distance *= 1.12;
        if (dto.AvoidTolls) distance *= 1.06;

        var route = new LogiTrack.Core.Entities.Route
        {
            Name = $"{dto.Origin} → {dto.Destination}",
            Origin = dto.Origin,
            Destination = dto.Destination,
            DistanceKm = Math.Round(distance, 1),
            EstimatedDurationMinutes = (int)Math.Round(distance / 55 * 60),
            FuelCostEstimate = Math.Round((decimal)distance * 8.5m, 2),
            IsOptimized = true,
            Waypoints = dto.Waypoints.Select((w, i) => new RouteWaypoint
            {
                Order = i + 1,
                Name = w.Name,
                Latitude = w.Latitude,
                Longitude = w.Longitude
            }).ToList()
        };

        db.Routes.Add(route);
        await db.SaveChangesAsync();

        return Ok(new RouteDto
        {
            Id = route.Id,
            Name = route.Name,
            Origin = route.Origin,
            Destination = route.Destination,
            DistanceKm = route.DistanceKm,
            EstimatedDurationMinutes = route.EstimatedDurationMinutes,
            FuelCostEstimate = route.FuelCostEstimate,
            IsOptimized = route.IsOptimized,
            Waypoints = route.Waypoints.Select(w => new WaypointDto { Order = w.Order, Name = w.Name, Latitude = w.Latitude, Longitude = w.Longitude }).ToList()
        });
    }
}

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] AppDbContext db) =>
        Ok(await db.Customers
            .Include(c => c.Shipments)
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                City = c.City,
                Country = c.Country,
                TotalShipments = c.Shipments.Count
            })
            .ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest req, [FromServices] AppDbContext db)
    {
        var customer = new Customer
        {
            Name = req.Name,
            Email = req.Email,
            Phone = req.Phone,
            Address = req.Address,
            City = req.City,
            Country = req.Country,
            IsActive = true
        };

        db.Customers.Add(customer);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address,
            City = customer.City,
            Country = customer.Country,
            TotalShipments = 0
        });
    }
}

public record CreateCustomerRequest(
    string Name,
    string Email,
    string Phone,
    string Address,
    string City,
    string Country);
