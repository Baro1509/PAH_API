﻿using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement {
    public class OrderService : IOrderService {
        private readonly IOrderDAO _orderDAO;
        private readonly IProductDAO _productDAO;
        private readonly IAddressDAO _addressDAO;
        private readonly IProductImageDAO _productImageDAO;

        public OrderService(IOrderDAO orderDAO, IProductDAO productDAO, IAddressDAO addressDAO, IProductImageDAO productImageDAO) {
            _orderDAO = orderDAO;
            _productDAO = productDAO;
            _addressDAO = addressDAO;
            _productImageDAO = productImageDAO;
        }

        public void ApproveCancelOrderRequest(int sellerId, int orderId) {
            throw new NotImplementedException();
        }

        public void CancelOrderRequest(int buyerId, int orderId) {
            throw new NotImplementedException();
        }

        public void Create(Order order) {
            _orderDAO.Create(order);
        }

        public void Create(List<int> productId, int buyerId, int addressId) {
            List<Product> products = new List<Product>();
            var address = _addressDAO.Get(addressId);
            foreach (int id in productId) {
                Product p = _productDAO.GetProductById(id);
                if (p == null) {
                    throw new Exception();
                }
                products.Add(p);
            }
            foreach (var productBySeller in products.GroupBy(p => p.SellerId)) {
                Order order = new Order {
                    BuyerId = buyerId,
                    SellerId = productBySeller.FirstOrDefault().SellerId,
                    RecipientName = address.RecipientName,
                    RecipientPhone = address.RecipientPhone,
                    RecipientAddress = address.Street + address.Ward + address.District + address.Province,
                    OrderDate = DateTime.Now,
                    Status = (int) OrderStatus.Pending,
                    ShippingCost = 0, //Update function later
                    TotalAmount = 0,
                    OrderItems = new List<OrderItem>()
                };
                productBySeller.ToList().ForEach(p => {
                    order.OrderItems.Add(new OrderItem {
                        ProductId = p.Id,
                        Price = p.Price,
                        Quantity = 1, //Update later base on business rule
                        ImageUrl = _productImageDAO.GetByProductId(p.Id).FirstOrDefault().ImageUrl
                    });
                    order.TotalAmount += p.Price;
                });
                _orderDAO.Create(order);
            }
        }

        public List<Order> GetAll() {
            return _orderDAO.GetAllOrder().ToList();
        }

        public List<Order> GetByBuyerId(int buyerId) {
            return _orderDAO.GetAllByBuyerId(buyerId).ToList();
        }

        public List<Order> GetBySellerId(int sellerId) {
            return _orderDAO.GetAllBySellerId(sellerId).ToList();
        }

        public Order UpdateOrderStatus(int sellerId, int status, int orderId) {
            var order = _orderDAO.Get(orderId);
            if (order == null) {
                throw new Exception("404: Order not found");
            }

            if (sellerId !=  order.SellerId) {
                throw new Exception("401: You are not allowed to update this order");
            }

            order.Status = status;
            _orderDAO.UpdateOrder(order);
            return _orderDAO.Get(orderId);
        }
    }
}
