﻿using API.Request;
using API.Response;
using API.Response.ProductRes;
using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] string? nameSearch, [FromQuery] int categoryId, [FromQuery] int type, [FromQuery] int condition, [FromQuery] int ratings, [FromQuery] decimal priceMin, [FromQuery] decimal priceMax, [FromQuery] int orderBy)
        {
            List<Product> productList = _productService.GetProducts(nameSearch, categoryId, type, condition, ratings, priceMin, priceMax, orderBy);
            List<ProductResponse> response = _mapper.Map<List<ProductResponse>>(productList);
            return Ok(new BaseResponse { Code = 200, Message = "Get products successfully", Data = response });
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Product product = _productService.GetProductById(id);
            ProductResponse response = _mapper.Map<ProductResponse>(product);
            return Ok(new BaseResponse { Code = 200, Message = "Get product successfully", Data = response });
        }

        [HttpPost]
        public IActionResult RegisterProduct([FromBody] ProductRequest request)
        {
            _productService.CreateProduct(_mapper.Map<Product>(request));
            return Ok(new BaseResponse { Code = 200, Message = "Register product successfully", Data = null });
        }

        //[HttpPatch]
        //public IActionResult EditProduct([FromQuery] int id, [FromBody] ProductRequest request)
        //{
        //    _productService.UpdateProduct(id, _mapper.Map<Product>(request));
        //    return Ok(new BaseResponse { Code = 200, Message = "Edit product successfully", Data = null });
        //}
    }
}
