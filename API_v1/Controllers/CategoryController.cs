using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Implement;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService = new CategoryService();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCategories() { 
            var result = _categoryService.GetCategories();
            return Ok(result);
        }
    }
}
