﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess {
    public interface IUserDAO {
        public User Get(int id);
        public User GetByEmail(string email);
        public IQueryable<User> GetAll();

        public void Register(User user);
    }
}
