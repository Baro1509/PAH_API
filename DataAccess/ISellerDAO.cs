﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISellerDAO
    {
        public Seller GetSeller(int id);
        public void CreateSeller(Seller seller);
    }
}
