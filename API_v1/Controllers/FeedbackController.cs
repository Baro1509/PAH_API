﻿using API.ErrorHandling;
using API.Request;
using API.Response;
using API.Response.FeedbackRes;
using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IFeedbackService _feedbackService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FeedbackController(IFeedbackService feedbackService, IMapper mapper, IUserService userService)
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
            _userService = userService;
        }

        private int GetUserIdFromToken()
        {
            var user = HttpContext.User;
            return int.Parse(user.Claims.FirstOrDefault(p => p.Type == "UserId").Value);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            Feedback feedback = _feedbackService.GetById(id);
            FeedbackResponse response = _mapper.Map<FeedbackResponse>(feedback);
            response.BuyerName = _userService.Get(response.BuyerId).Name;
            return Ok(new BaseResponse { Code = (int)HttpStatusCode.OK, Message = "Get feedback successfully", Data = response });
        }

        [HttpGet]
        [Route("product")]
        public IActionResult GetAll(int productId)
        {
            List<Feedback> feedbackList = _feedbackService.GetAll(productId);
            List<FeedbackResponse> responses = _mapper.Map<List<FeedbackResponse>>(feedbackList);
            foreach (FeedbackResponse response in responses)
            {
                response.BuyerName = _userService.Get(response.BuyerId).Name;
            }
            return Ok(new BaseResponse { Code = (int)HttpStatusCode.OK, Message = "Get all feedbacks successfully", Data = responses });
        }

        [HttpPost]
        public IActionResult Create([FromBody] FeedbackRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _feedbackService.CreateFeedback(_mapper.Map<Feedback>(request));
            return Ok(new BaseResponse { Code = (int)HttpStatusCode.OK, Message = "Feedback successfully", Data = null });
        }
    }
}
