using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace JwtRoleAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AdminController : ControllerBase
    {
        [HttpGet("dashboard")]
        [Authorize(Roles = "Admin")]

        public IActionResult GetAdminDashboard()
        {
            return Ok("Welcome to the admin Dashboard! only users with the admin role can see this");
        }
    }
}