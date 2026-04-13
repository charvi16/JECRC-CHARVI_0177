using Microsoft.AspNetCore.Mvc;
using EmployeePortal.Entities;
using EmployeePortal.Models.Dto;

namespace EmployeePortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>();
        private static int idCounter = 1;

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new Employee
            {
                Id = idCounter++,
                Name = dto.Name,
                Salary = dto.Salary,
                Email = dto.Email,
                Password = dto.Password,
                Phone = dto.Phone
            };

            employees.Add(employee);

            return Ok(employee);
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(employees);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();

            employees.Remove(emp);
            return Ok("Deleted");
        }
    }
}