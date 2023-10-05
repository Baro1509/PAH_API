using API.ErrorHandling;
using API.Request;
using API.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Net;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService) {
            _addressService = addressService;
        }

        private int GetUserIdFromToken() {
            var user = HttpContext.User;
            return int.Parse(user.Claims.FirstOrDefault(p => p.Type == "UserId").Value);
        }

        [HttpGet]
        public IActionResult GetByCustomerId() {
            var id = GetUserIdFromToken();
            return Ok(new BaseResponse { Code = (int) HttpStatusCode.OK, Message = "Get address by customer successfully", Data = _addressService.GetByCustomerId(id) });
        }
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] AddressRequest request) {
            if (!ModelState.IsValid) {
                return Ok(new ErrorDetails { StatusCode = (int) HttpStatusCode.OK, Message = ModelState.ToString()});
            }
            var id = GetUserIdFromToken();
            return Ok(new BaseResponse { Code = (int) HttpStatusCode.OK, Message = "Get address by customer successfully", Data = _addressService.GetByCustomerId(id) });
        }
    }
}
