using Microsoft.AspNetCore.Mvc;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET all tasks
        [HttpGet]
        public IActionResult GetTodos()
        {
            return Ok(_context.Todos.ToList());
        }

        // ADD task
        [HttpPost]
        public IActionResult AddTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();

            return Ok(todo);
        }

        // UPDATE completion
        [HttpPut("{id}")]
        public IActionResult ToggleTodo(int id)
        {
            var todo = _context.Todos.Find(id);

            if (todo == null) return NotFound();

            todo.IsCompleted = !todo.IsCompleted;

            _context.SaveChanges();

            return Ok(todo);
        }

        [HttpPut("edit/{id}")]
        public IActionResult EditTodo(int id, Todo updatedTodo)
        {
            var todo = _context.Todos.Find(id);

            if (todo == null)
                return NotFound();

            todo.Title = updatedTodo.Title;
            todo.Priority = updatedTodo.Priority;

            _context.SaveChanges();

            return Ok(todo);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var todo = _context.Todos.Find(id);

            if (todo == null) return NotFound();

            _context.Todos.Remove(todo);

            _context.SaveChanges();

            return Ok();
        }
    }
}