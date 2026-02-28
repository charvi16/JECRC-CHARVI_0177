using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Services;
using EmployeeAPI.Models;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        EmployeeService service =
        new EmployeeService();

        [HttpGet]

        public List<Employee> Get()
        {
            return service.GetEmployees();
        }


        [HttpPost]

        public void Post(Employee emp)
        {
            service.AddEmployee(emp);
        }

    }
}