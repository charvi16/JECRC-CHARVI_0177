using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmpSinMemoryApi.Repository;
using EmpSinMemoryApi.Models;


namespace EmpSinMemoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmmployeesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(EmployeeRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var employee = EmployeeRepository.GetbyId(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            EmployeeRepository.Add(employee);
            return CreatedAtAction(nameof(GetbyId), new {id = employee.Id}, employee);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee employee)
        {
            var updated = EmployeeRepository.Update(id, employee);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = EmployeeRepository.Delete(id);
            return deleted? NoContent() : NotFound();
        }
    }
}