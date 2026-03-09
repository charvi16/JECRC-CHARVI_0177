using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ProductController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product updatedProduct)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if(product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("search")]
            public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string name)
            {
                var products = await _context.Products
                    .Where(p => p.Name.Contains(name))
                    .ToListAsync();

                return Ok(products);
            }
    }
}