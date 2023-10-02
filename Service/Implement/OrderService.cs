using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement {
    public class OrderService : IOrderService {
        private readonly IOrderDAO _orderDAO;

        public OrderService(IOrderDAO orderDAO) {
            _orderDAO = orderDAO;
        }

        public void ApproveCancelOrderRequest(int sellerId, int orderId) {
            throw new NotImplementedException();
        }

        public void CancelOrderRequest(int buyerId, int orderId) {
            throw new NotImplementedException();
        }

        public void Create(Order order) {
            throw new NotImplementedException();
        }

        public List<Order> GetAll() {
            throw new NotImplementedException();
        }

        public List<Order> GetByBuyerId(int buyerId) {
            throw new NotImplementedException();
        }

        public List<Order> GetBySellerId(int sellerId) {
            throw new NotImplementedException();
        }

        public Order UpdateOrderStatus(int sellerId, int status, int orderId) {
            throw new NotImplementedException();
        }
    }
}
