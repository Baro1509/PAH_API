﻿using API.ErrorHandling;
using Microsoft.AspNetCore.Cors;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Request.ThirdParty.Zalopay;
using System.Net;
using DataAccess;
using Respon;
using Respon.WalletRes;
using Request;
using Request.Param;
using Respon.OrderRes;
using System.Collections.Generic;
using Respon.AuctionRes;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors]
    public class WalletController : ControllerBase {
        private readonly IWalletService _walletService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public WalletController(IWalletService walletService, IUserService userService, IMapper mapper) {
            _walletService = walletService;
            _userService = userService;
            _mapper = mapper;
        }

        private int GetUserIdFromToken()
        {
            var user = HttpContext.User;
            return int.Parse(user.Claims.FirstOrDefault(p => p.Type == "UserId").Value);
        }

        [HttpPost("topup")]
        public async Task<IActionResult> Topup([FromBody] TopupRequest request) {
            var userId = GetUserIdFromToken();  
            if (userId == null) {
                return Unauthorized(new ErrorDetails { 
                    StatusCode = (int) HttpStatusCode.Unauthorized, 
                    Message = "You are not logged in" 
                });
            }

            var user = _userService.Get(userId);
            if (user == null) {
                return Unauthorized(new ErrorDetails { 
                    StatusCode = (int) HttpStatusCode.Unauthorized, 
                    Message = "You are not allowed to access this" 
                });
            }

            await _walletService.Topup(userId, request);
            return Ok(new BaseResponse { 
                Code = (int) HttpStatusCode.OK, 
                Message = "Topup successfully", 
                Data = null 
            });
        }

        [HttpPost("payment/order/{orderId:int}")]
        public IActionResult Pay(int orderId) {
            var userId = GetUserIdFromToken();
            if (userId == null) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not logged in"
                });
            }
            var user = _userService.Get(userId);
            if (user == null) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "Your account is not available"
                });
            }
            if (user.Role != (int) Role.Buyer && user.Role != (int) Role.Seller) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            _walletService.CheckoutWallet(userId, orderId, (int) OrderStatus.WaitingSellerConfirm);
            return Ok(new BaseResponse {
                Code = (int) HttpStatusCode.OK,
                Message = $"Pay for order {orderId} successfully",
                Data = null
            });
        }

        [HttpGet("current")]
        public IActionResult GetByCurrentUser()
        {
            var userId = GetUserIdFromToken();
            Wallet wallet = _walletService.GetByCurrentUser(userId);
            if (wallet == null)
            {
                wallet = new Wallet();
            }
            WalletCurrentUserResponse response = _mapper.Map<WalletCurrentUserResponse>(wallet);
            return Ok(new BaseResponse
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Get current user wallet successfully",
                Data = response
            });
        }

        [HttpPost("withdraw")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public IActionResult Withdrawal([FromBody] WithdrawalRequest request) {
            var userId = GetUserIdFromToken();
            if (userId == null) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not logged in"
                });
            }

            var user = _userService.Get(userId);
            if (user == null) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            if (user.Role != (int) Role.Seller && user.Role != (int) Role.Buyer) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            _walletService.CreateWithdrawal(userId, request);
            return Ok(new BaseResponse {
                Code = (int) HttpStatusCode.OK,
                Message = "Create withdrawal successfully",
                Data = null
            });
        }
        
        [HttpGet("withdraw")]
        public IActionResult GetWithdrawal([FromQuery] PagingParam pagingParam) {
            var userId = GetUserIdFromToken();
            if (userId == null) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not logged in"
                });
            }

            var user = _userService.Get(userId);
            if (user == null) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            
            var data = _walletService.GetWithdrawalByUserId(userId).Skip((pagingParam.PageNumber - 1) * pagingParam.PageSize)
                .Take(pagingParam.PageSize).ToList();
            List<WithdrawalResponse> mappedList = _mapper.Map<List<WithdrawalResponse>>(data);

            return Ok(new BaseResponse {
                Code = (int) HttpStatusCode.OK,
                Message = "Get withdrawal successfully",
                Data = mappedList
            });
        }

        [HttpGet("manager/withdraw")]
        public IActionResult GetWithdrawalManager([FromQuery] PagingParam pagingParam)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "You are not logged in"
                });
            }

            var user = _userService.Get(userId);
            if (user == null)
            {
                return Unauthorized(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            if (user.Role != (int)Role.Manager)
            {
                return Unauthorized(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            var data = _walletService.GetWithdrawalManager().Skip((pagingParam.PageNumber - 1) * pagingParam.PageSize)
                .Take(pagingParam.PageSize).ToList();
            List<WithdrawalResponse> mappedList = _mapper.Map<List<WithdrawalResponse>>(data);
            return Ok(new BaseResponse
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Get withdrawal successfully",
                Data = mappedList
            });
        }

        [HttpPost("manager/withdraw")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public IActionResult UpdateWithdrawal([FromBody] UpdateWithdrawRequest request) {
            var userId = GetUserIdFromToken();
            if (userId == null) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not logged in"
                });
            }

            var user = _userService.Get(userId);
            if (user == null) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            if (user.Role != (int) Role.Manager) {
                return Unauthorized(new ErrorDetails {
                    StatusCode = (int) HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            if (request.Status == (int) WithdrawalStatus.Done) {
                _walletService.ApproveWithdrawal(request.WithdrawalId, userId);
            } else {
                _walletService.DenyWithdrawal(request.WithdrawalId, userId);
            }
            return Ok(new BaseResponse {
                Code = (int) HttpStatusCode.OK,
                Message = "Update withdrawal successfully",
                Data = null
            });
        }
    }
}
