﻿using API.Response;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Category> categoryList = _categoryService.GetAll();
            return Ok(new BaseResponse { Code = (int)HttpStatusCode.OK, Message = "Get all categories successfully", Data = categoryList });
        }
    }
}
