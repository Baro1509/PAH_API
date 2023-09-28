using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class CategoryService : ICategoryService
    {
        public List<Category> GetCategories()
        {
            return ICategoryDAO.GetCategories();
        }
    }
}
