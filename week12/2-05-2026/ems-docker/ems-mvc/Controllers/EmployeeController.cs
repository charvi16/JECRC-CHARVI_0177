using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using ems_mvc.Models;

namespace ems_mvc.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public EmployeesController(IConfiguration config)
        {
            _http = new HttpClient();
            _baseUrl = config["ApiBaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var res = await _http.GetStringAsync($"{_baseUrl}/api/employees");

            Console.WriteLine("API RESPONSE: " + res);

            var data = JsonSerializer.Deserialize<List<Employee>>(res, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data == null)
                return Content("No data received from API");

            return View(data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            var json = JsonSerializer.Serialize(emp);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _http.PostAsync($"{_baseUrl}/api/employees", content);
            return RedirectToAction("Index");
        }
    }
}