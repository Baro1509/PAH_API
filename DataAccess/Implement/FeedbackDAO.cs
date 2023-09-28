﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implement
{
    public class FeedbackDAO : DataAccessBase<Feedback>, IFeedbackDAO
    {
        public FeedbackDAO(PlatformAntiquesHandicraftsContext context): base(context) { }

        public Feedback GetByProductId(int productId)
        {
            return GetAll().FirstOrDefault(f => f.ProductId == productId && f.Status == (int)Status.Available);
        }

        public void CreateFeedback(Feedback feedback)
        {
            Create(feedback);
        }

        public void Update(Feedback feedback)
        {
            Update(feedback);
        }

        public void Delete(Feedback feedback)
        {
            Delete(feedback);
        }
    }
}
