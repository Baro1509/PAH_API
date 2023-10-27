﻿using API.ErrorHandling;
using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Param;
using Respon;
using Respon.TransactionRes;
using Service;
using Service.Implement;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper, IUserService userService)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _userService = userService;
        }

        private int GetUserIdFromToken()
        {
            var user = HttpContext.User;
            return int.Parse(user.Claims.FirstOrDefault(p => p.Type == "UserId").Value);
        }

        [Authorize]
        [HttpGet("current")]
        public IActionResult GetTransactionCurrentUser([FromQuery] int type, [FromQuery] int orderBy, [FromQuery] PagingParam pagingParam)
        {
            var userId = GetUserIdFromToken();
            var user = _userService.Get(userId);
            if (user == null || (user.Role != (int)Role.Buyer && user.Role != (int)Role.Seller))
            {
                return Unauthorized(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            List<Transaction> transactions = _transactionService.GetTransactionsByUserId(userId, type, orderBy)
                .Skip((pagingParam.PageNumber - 1) * pagingParam.PageSize).Take(pagingParam.PageSize).ToList();

            List<TransactionResponse> responses = _mapper.Map<List<TransactionResponse>>(transactions);

            return Ok(new BaseResponse
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Get current user's transactions successfully",
                Data = responses
            });
        }

        [Authorize]
        [HttpGet("current")]
        public IActionResult GetAllTransactions([FromQuery] PagingParam pagingParam)
        {
            var userId = GetUserIdFromToken();
            var user = _userService.Get(userId);
            if (user == null || (user.Role != (int)Role.Manager && user.Role != (int)Role.Administrator))
            {
                return Unauthorized(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "You are not allowed to access this"
                });
            }
            List<Transaction> transactions = _transactionService.GetAllTransactions()
                .Skip((pagingParam.PageNumber - 1) * pagingParam.PageSize).Take(pagingParam.PageSize).ToList();

            List<TransactionResponse> responses = _mapper.Map<List<TransactionResponse>>(transactions);

            return Ok(new BaseResponse
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Get transactions successfully",
                Data = responses
            });
        } 
    }
}
