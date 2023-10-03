﻿using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class ProductService : IProductService
    {
        private readonly IProductDAO _productDAO;

        public ProductService(IProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        public List<Product> GetProducts(string? nameSearch, int materialId, int categoryId, decimal priceMin, decimal priceMax, int orderBy)
        {
            List<Product> productList;
            try
            {
                var products = _productDAO.GetProducts()
                    .Where(p => p.Status == (int)Status.Available
                    //&& p. == (int)Status.Available
                    && (string.IsNullOrEmpty(nameSearch) || p.Name.Contains(nameSearch))
                    && (materialId == 0 || p.MaterialId == materialId)
                    && (categoryId == 0 || p.CategoryId == categoryId)
                    //&& (type == 0 || p.Type == type)
                    //&& (condition == 0 || p.Condition == condition)
                    //&& (ratings == 0 || p.Ratings == ratings)
                    && p.Price >= priceMin
                    && (priceMax == 0 || p.Price <= priceMax));

                //default (0): new -> old , 1: old -> new, 2: low -> high, 3: high -> low
                switch (orderBy)
                {
                    case 1:
                        products = products.OrderByDescending(p => p.Condition);
                        break;
                    case 2:
                        products = products.OrderBy(p => p.Price);
                        break;
                    case 3:
                        products = products.OrderByDescending(p => p.Price);
                        break;
                    default:
                        products = products.OrderBy(p => p.Condition);
                        break;
                }

                productList = products
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return productList;
        }

        public Product GetProductById(int id)
        {
            if (id == null)
            {
                throw new Exception("404: Product not found");
            }
            return _productDAO.GetProductById(id);
        }

        public List<Product> GetProductsBySellerId(int sellerId)
        {
            if (sellerId == null)
            {
                throw new Exception("404: Seller not found");
            }
            return _productDAO.GetProductsBySellerId(sellerId).ToList();
        }

        public void CreateProduct(Product product)
        {
            product.Status = (int)Status.Available;
            product.Ratings = 0;
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _productDAO.CreateProduct(product);
        }

        public Product UpdateProduct(int id, Product product)
        {
            if (id == null) throw new Exception("404: Product not found");

            Product currentProduct = _productDAO.GetProductById(id);

            currentProduct.CategoryId = product.CategoryId;
            currentProduct.MaterialId = product.MaterialId;
            currentProduct.Name = product.Name;
            currentProduct.Description = product.Description;
            currentProduct.Price = product.Price;
            currentProduct.Dimension = product.Dimension;
            currentProduct.Weight = product.Weight;
            currentProduct.Origin = product.Origin;
            currentProduct.PackageMethod = product.PackageMethod;
            currentProduct.PackageContent = product.PackageContent;
            currentProduct.Condition = product.Condition;
            currentProduct.Type = product.Type;
            currentProduct.UpdatedAt = DateTime.Now;

            _productDAO.UpdateProduct(currentProduct);

            return currentProduct;
        }

        public Product DeleteProduct(int id)
        {
            if (id == null) throw new Exception("404: Product not found");

            Product currentProduct = _productDAO.GetProductById(id);

            currentProduct.Status = (int)Status.Unavailable;
            currentProduct.UpdatedAt = DateTime.Now;

            _productDAO.UpdateProduct(currentProduct);

            return currentProduct;
        }
    }
}
