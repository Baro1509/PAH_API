using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class MaterialService : IMaterialService
    {
        public List<Material> GetMaterials()
        {
            return IMaterialDAO.GetMaterials();
        }
    }
}
