using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IFeedbackDAO
    {
        public Feedback GetByProductId(int productId);
        public void Create(Feedback feedback);
        public void Update(Feedback feedback);
        public void Delete(Feedback feedback);
    }
}
