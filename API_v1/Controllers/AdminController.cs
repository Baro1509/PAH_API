using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase {
        [HttpPost("staff")]
        public IActionResult AddStaff() {
            return Ok();
        }
        
        [HttpPatch("staff")]
        public IActionResult EditStaff() {
            return Ok();
        }

        [HttpGet("account")]
        public IActionResult ViewAllAccount() {
            return Ok();
        }
        
        [HttpPatch("account")]
        public IActionResult EditStatusAccount() {
            return Ok();
        }
    }
}
