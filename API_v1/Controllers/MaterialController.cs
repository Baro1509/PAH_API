using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private IMaterialService _materialService = new MaterialService();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCategories()
        {
            var result = _materialService.GetMaterials();
            return Ok(result);
        }
    }
}
