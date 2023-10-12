﻿using Service.ThirdParty.Zalopay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IWalletService {
        public Task Topup(int userId, TopupRequest orderRequest);
        public void CheckoutWallet(int userId, int orderId);
        //public void CheckoutZalopay(int userId, int orderId, TopupRequest orderRequest);
        public void AddLockedBalance(int userId, decimal balance);
        public void SubtractLockedBalance(int userId, decimal balance);
    }
}
