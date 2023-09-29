using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IProductService
    {
        public List<Product> GetProducts(string? nameSearch, int categoryId, int type, int condition, int ratings, decimal priceMin, decimal priceMax, int orderBy);
        public Product GetProductById(int id);
        public void CreateProduct(Product product);
        public void UpdateProduct(int id, Product product);
    }
}
