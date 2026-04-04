using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiCatalogBillGenerator.Data;
using MultiCatalogBillGenerator.DTOs;
using MultiCatalogBillGenerator.Models;

namespace MultiCatalogBillGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public InvoiceController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _db.Invoices
                .Include(x => x.Items)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _db.Invoices
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceRequest request)
        {
            if (request.Items == null || request.Items.Count == 0)
                return BadRequest("Invoice must contain at least one item.");

            var invoice = new Invoice
            {
                InvoiceNumber = await GenerateInvoiceNumber(),
                CreatedAt = DateTime.Now,
                Status = "Finalized",
                DiscountType = request.DiscountType,
                DiscountValue = request.DiscountValue
            };

            decimal subtotal = 0;
            decimal taxAmount = 0;

            foreach (var item in request.Items)
            {
                var lineSubtotal = item.UnitPrice * item.Quantity;
                var lineTax = lineSubtotal * (item.TaxPercentage / 100);
                var lineTotal = lineSubtotal + lineTax;

                invoice.Items.Add(new InvoiceItem
                {
                    ItemName = item.ItemName,
                    CatalogType = item.CatalogType,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TaxPercentage = item.TaxPercentage,
                    LineTotal = lineTotal,
                    IsCustomItem = item.IsCustomItem
                });

                subtotal += lineSubtotal;
                taxAmount += lineTax;
            }

            decimal discountAmount = 0;

            if (invoice.DiscountType == "percent")
                discountAmount = subtotal * (invoice.DiscountValue / 100);
            else if (invoice.DiscountType == "fixed")
                discountAmount = invoice.DiscountValue;

            if (discountAmount > subtotal + taxAmount)
                discountAmount = subtotal + taxAmount;

            invoice.Subtotal = subtotal;
            invoice.TaxAmount = taxAmount;
            invoice.DiscountAmount = discountAmount;
            invoice.GrandTotal = subtotal + taxAmount - discountAmount;

            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();

            return Ok(invoice);
        }

        [HttpGet("daily-summary")]
        public async Task<IActionResult> GetDailySummary([FromQuery] DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1);

            var invoices = await _db.Invoices
                .Where(x => x.CreatedAt >= start && x.CreatedAt < end)
                .ToListAsync();

            var result = new DailySummaryResponse
            {
                TotalBills = invoices.Count,
                TotalRevenue = invoices.Sum(x => x.GrandTotal),
                TotalTax = invoices.Sum(x => x.TaxAmount),
                TotalDiscount = invoices.Sum(x => x.DiscountAmount)
            };

            return Ok(result);
        }

        private async Task<string> GenerateInvoiceNumber()
        {
            var today = DateTime.Now.ToString("yyyyMMdd");
            var countToday = await _db.Invoices.CountAsync(x => x.CreatedAt.Date == DateTime.Now.Date);
            var serial = (countToday + 1).ToString("D4");
            return $"INV-{today}-{serial}";
        }
    }
}