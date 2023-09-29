using API.Request;
using API.Response.ProductRes;
using AutoMapper;
using DataAccess.Models;

namespace API.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
        }

        public static ProductResponse ToProductResponse(Product product)
        {
            if (product == null)
            {
                return null;
            }

            return new ProductResponse()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Dimension = product.Dimension,
                Weight = product.Weight,
                Origin = product.Origin,
                PackageMethod = product.PackageMethod,
                PackageContent = product.PackageContent,
                Condition = product.Condition,
                Ratings = product.Ratings,
                CategoryName = product.Category.Name,
                MaterialName = product.Material.Name,
                SellerName = product.Seller.Name
            };
        }
    }
}
