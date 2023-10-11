﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess {
    public interface ITransactionDAO {
        public void Create(Transaction transaction);
        public IQueryable<Transaction> GetByUserId(int userId);
        public bool IsZalopayOrderValid(string appTransId, string mac);
    }
}
