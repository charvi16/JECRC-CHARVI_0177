using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace JwtRoleAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ManagerController : ControllerBase
    {
        [HttpGet("dashboard")]
        [Authorize(Roles = "Manager")]

        public IActionResult GetAdminDashboard()
        {
            return Ok("Welcome to the admin Dashboard! only users with the admin role can see this");
        }

        [HttpGet("reports")]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult GetReports()
        {
            return Ok("Welcome to Admin and Manager Reports!Only users with admin and manager roles can access this");
        }
    }
}