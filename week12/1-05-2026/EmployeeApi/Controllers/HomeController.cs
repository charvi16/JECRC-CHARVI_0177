using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}