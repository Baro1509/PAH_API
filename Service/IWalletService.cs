using Service.ThirdParty.Zalopay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public interface IWalletService {
        public void Topup(int userId, OrderRequest orderRequest);
        public void CheckoutWallet(int userId, int orderId);
        public void CheckoutZalopay(int userId, int orderId, OrderRequest orderRequest);
        public void AddLockedBalance(int userId, decimal balance);
        public void SubtractLockedBalance(int userId, decimal balance);
    }
}
