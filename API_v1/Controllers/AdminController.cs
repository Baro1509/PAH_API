using API.ErrorHandling;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request;
using Respon;
using Service;
using System.Net;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IAdminService _adminService;

        public AdminController(IMapper mapper, IAdminService adminService) {
            _mapper = mapper;
            _adminService = adminService;
        }

        [HttpPost("staff")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public IActionResult AddStaff([FromBody] StaffRequest request) {
            _adminService.CreateStaff(_mapper.Map<User>(request));
            return Ok(new BaseResponse {
                Code = (int) HttpStatusCode.OK,
                Message = "Create staff successfully",
                Data = null
            });
        }
        
        [HttpPatch("staff")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public IActionResult EditStaff([FromBody] StaffRequest request) {
            _adminService.UpdateStaff(_mapper.Map<User>(request));
            return Ok(new BaseResponse {
                Code = (int) HttpStatusCode.OK,
                Message = "Create staff successfully",
                Data = null
            });
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
