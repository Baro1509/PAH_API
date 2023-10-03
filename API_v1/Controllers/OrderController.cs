using API.ErrorHandling;
using API.Request;
using API.Response;
using AutoMapper.Configuration.Conventions;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Net;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IUserService userService) {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(_orderService.GetAll());
        }

        [HttpGet("/api/buyer/order")]
        public IActionResult GetByBuyerId() {
            var id = GetUserIdFromToken();
            var user = _userService.Get(id);

            if (user == null) {
                return Unauthorized(new ErrorDetails { StatusCode = (int) HttpStatusCode.Unauthorized, Message = "You are not allowed to access this" });
            }

            var orders = _orderService.GetByBuyerId(id);
            return Ok(new BaseResponse { Code = (int) HttpStatusCode.OK, Message = "Get Buyer's order list successfully", Data = orders});
        }
        
        [HttpGet("/api/seller/order")]
        public IActionResult GetBySellerId() {
            var id = GetUserIdFromToken();
            var user = _userService.Get(id);

            if (user == null) {
                return Unauthorized(new ErrorDetails { StatusCode = (int) HttpStatusCode.Unauthorized, Message = "You are not allowed to access this" });
            }

            var orders = _orderService.GetBySellerId(id);
            return Ok(new BaseResponse { Code = (int) HttpStatusCode.OK, Message = "Get Seller's order list successfully", Data = orders});
        }

        [HttpPost("/api/buyer/checkout")]
        public IActionResult Checkout([FromBody] CheckoutRequest request) {
            var id = GetUserIdFromToken();
            var user = _userService.Get(id);

            if (user == null) {
                return Unauthorized(new ErrorDetails { StatusCode = (int) HttpStatusCode.Unauthorized, Message = "You are not allowed to access this" });
            }
            _orderService.Create(request.ProductIds, user.Id, request.AddressId);
            return Ok(new BaseResponse { Code = (int) HttpStatusCode.OK, Message = "Checkout successfully", Data = null });
        }

        private int GetUserIdFromToken() {
            var user = HttpContext.User;
            return int.Parse(user.Claims.FirstOrDefault(p => p.Type == "UserId").Value);
        }
    }
}
