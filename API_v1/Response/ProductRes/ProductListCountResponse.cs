﻿namespace API.Response.ProductRes
{
    public class ProductListCountResponse
    {
        public int Count { get; set; }
        public List<ProductListResponse> ProductList { get; set; }
    }
}
