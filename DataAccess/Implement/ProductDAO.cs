using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implement
{
    public class ProductDAO : DataAccessBase<Product>, IProductDAO
    {
        public ProductDAO(PlatformAntiquesHandicraftsContext context) : base(context) { }

        public IQueryable<Product> GetProducts()
        {
            return GetAll().Include(c => c.Category)
                    .Include(m => m.Material)
                    .Include(s => s.Seller);
            //List<Product> productList;
            //try
            //{
            //    var products = GetAll()
            //        .Where(p => p.Status == (int)Status.Available
            //        //&& p.Seller.Id == (int)Status.Available
            //        //&& (string.IsNullOrEmpty(nameSearch) || p.Name.Contains(nameSearch))
            //        && (categoryId == 0 || p.CategoryId == categoryId));
            //        //&& p.Price >= priceMin
            //        //&& (priceMax == 0 || p.Price <= priceMax));

            //    //default (0): created at desc, 1: created at asc, 2: price desc, 3: price asc
            //    switch (orderBy)
            //    {
            //        case 1:
            //            products = products.OrderBy(p => p.CreatedAt);
            //            break;
            //        case 2:
            //            products = products.OrderByDescending(p => p.Price);
            //            break;
            //        case 3:
            //            products = products.OrderBy(p => p.Price);
            //            break;
            //        default:
            //            products = products.OrderByDescending(p => p.CreatedAt);
            //            break;
            //    }

            //    productList = products.Include(c => c.Category).Include(s => s.Seller).ToList();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            //return productList;
        }

        public Product GetProductById(int id)
        {
            return GetAll().Include(c => c.Category)
                    .Include(m => m.Material)
                    .Include(s => s.Seller).FirstOrDefault(p => p.Id == id);
        }

        public void CreateProduct(Product product)
        {
            Create(product);
        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }
        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
