using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TasneemSami.ToDoList.Api.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        [HttpGet("only")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetAdminData()
        {
            return Ok("✅ Welcome, Admin! You have access to this protected route.");
        }
    }
}
