using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // 1) Create transaction
        // POST: api/transactions
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }

        // 2) Get transaction by id
        // GET: api/transactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
                return NotFound(new { message = "Transaction not found" });

            return Ok(transaction);
        }

        // 3) Get all transactions in retrieved order
        // GET: api/transactions
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        // 4) Filter by date
        // GET: api/transactions/filter?date=2019-12-03
        [HttpGet("filter")]
        public async Task<IActionResult> FilterByDate([FromQuery] string? date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return Ok(await _context.Transactions.ToListAsync());

            DateTime parsedDate = DateTime.Parse(date);

            var result = await _context.Transactions
                .Where(t => t.Date.Date == parsedDate.Date)
                .ToListAsync();

            return Ok(result);
        }

        // 5) Sort by amount ascending
        // GET: api/transactions/sort/amount
        [HttpGet("sort/amount")]
        public async Task<IActionResult> SortByAmountAscending()
        {
            var sortedTransactions = await _context.Transactions
                .OrderBy(t => t.Amount)
                .ToListAsync();

            return Ok(sortedTransactions);
        }

        // 6) Combined endpoint: filter + sort
        // GET: api/transactions/search?date=2019-12-03&sortBy=amount
        // [HttpGet("search")]
        // public async Task<IActionResult> SearchTransactions([FromQuery] string? date, [FromQuery] string? sortBy)
        // {
        //     IQueryable<Transaction> query = _context.Transactions;

        //     if (!string.IsNullOrWhiteSpace(date))
        //     {
        //         query = query.Where(t => t.Date == date);
        //     }

        //     if (!string.IsNullOrWhiteSpace(sortBy) && sortBy.ToLower() == "amount")
        //     {
        //         query = query.OrderBy(t => t.Amount);
        //     }

        //     var result = await query.ToListAsync();
        //     return Ok(result);
        // }
    }
}