using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiCatalogBillGenerator.Data;
using MultiCatalogBillGenerator.Models;

namespace MultiCatalogBillGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CatalogController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _db.CatalogItems
                .Where(x => x.IsActive)
                .OrderBy(x => x.CatalogType)
                .ThenBy(x => x.Name)
                .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CatalogItem item)
        {
            _db.CatalogItems.Add(item);
            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CatalogItem updated)
        {
            var item = await _db.CatalogItems.FindAsync(id);
            if (item == null) return NotFound();

            item.Name = updated.Name;
            item.CatalogType = updated.CatalogType;
            item.DefaultPrice = updated.DefaultPrice;
            item.TaxPercentage = updated.TaxPercentage;
            item.IsCustomPriceAllowed = updated.IsCustomPriceAllowed;
            item.IsActive = updated.IsActive;

            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.CatalogItems.FindAsync(id);
            if (item == null) return NotFound();

            item.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok(new { message = "Catalog item deactivated" });
        }
    }
}