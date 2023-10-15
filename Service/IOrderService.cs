﻿using DataAccess.Models;
using Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public interface IOrderService {
        public void Create(Order order);
        public void Checkout(CheckoutRequest request, int buyerId, int addressId);
        public Order UpdateOrderStatus(int sellerId, int status, int orderId);
        public void CancelOrderRequest(int buyerId, int orderId);
        public void ApproveCancelOrderRequest(int sellerId, int orderId);
        public void SellerCancelOrder(int sellerId, int orderId, string reason);

        public List<Order> GetByBuyerId(int buyerId);
        public List<Order> GetBySellerId(int sellerId);
        public List<Order> GetAll();
    }
}
