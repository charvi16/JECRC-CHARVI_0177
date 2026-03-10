using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace JwtRoleAuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        [HttpGet("dashboard")]
        [Authorize(Roles = "User")]

        public IActionResult GetUserDashboard()
        {
            return Ok("Welcome to the user Dashboard! only users can see this");
        }
    }
}