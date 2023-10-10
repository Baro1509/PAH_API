using DataAccess;
using Service.ThirdParty;
using Service.ThirdParty.Zalopay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement {
    public class WalletService : IWalletService {
        private readonly IWalletDAO _walletDAO;
        private readonly ITransactionDAO _transactionDAO;
        private readonly IHttpClientFactory _httpClientFactory;

        public WalletService(IWalletDAO walletDAO, ITransactionDAO transactionDAO, IHttpClientFactory httpClientFactory) {
            _walletDAO = walletDAO;
            _transactionDAO = transactionDAO;
            _httpClientFactory = httpClientFactory;
        }

        public async void Topup(int userId, OrderRequest orderRequest) {
            var wallet = _walletDAO.Get(userId);
            if (wallet == null) {
                throw new Exception("404: Wallet not found");
            }

            var httpClient = _httpClientFactory.CreateClient("Zalopay");
            var data = new QueryRequest();
            data.SetDataInProcess1();
            var httpResponseMessage = await httpClient.PostAsync("query", Utils.ConvertForPost<QueryRequest>(data));
            
            if (!httpResponseMessage.IsSuccessStatusCode) {
                throw new Exception("400: No order in zalo pay yet");
            }

            var responseData = await httpResponseMessage.Content.ReadAsAsync<QueryResponse>();
            if (responseData.return_code != 1) {
                throw new Exception("400: " + responseData.return_message);
            }
            if (responseData.amount != orderRequest.Topup) {
                throw new Exception("400: Amount does not match with order from zalopay");
            }

            wallet.AvailableBalance += orderRequest.Topup;
            _walletDAO.Update(wallet);
            _transactionDAO.Create(new DataAccess.Models.Transaction {
                Id = 0,
                WalletId = wallet.Id,
                PaymentMethod = 1
            });
            
        }

        public void AddLockedBalance(int userId, decimal balance) {
            throw new NotImplementedException();
        }

        public void SubtractBalance(int userId, decimal balance) {
            throw new NotImplementedException();
        }

        public void SubtractLockedBalance(int userId, decimal balance) {
            throw new NotImplementedException();
        }
    }
}
