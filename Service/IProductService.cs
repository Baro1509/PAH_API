﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IProductService
    {
        public List<Product> GetProducts(string? nameSearch, int materialId, int categoryId, decimal priceMin, decimal priceMax, int orderBy);
        public Product GetProductById(int id);
        public List<Product> GetProductsBySellerId(int sellerId);
        public void CreateProduct(Product product, Auction auction);
        public Product UpdateProduct(int id, Product product);
        public Product DeleteProduct(int id);
    }
}
