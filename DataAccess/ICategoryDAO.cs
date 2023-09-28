using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ICategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var categoryList = new List<Category>();
            try
            {
                using (var context = new PlatformAntiquesHandicraftsContext())
                {
                    categoryList = context.Categories.ToList();
                }
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categoryList;
        }
    }
}
