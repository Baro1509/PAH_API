using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class IMaterialDAO
    {
        public static List<Material> GetMaterials()
        {
            var materialList = new List<Material>();
            try
            {
                using (var context = new PlatformAntiquesHandicraftsContext())
                {
                    materialList = context.Materials.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return materialList;
        }
    }
}
