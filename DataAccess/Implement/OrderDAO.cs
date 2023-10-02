using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implement {
    public class OrderDAO : DataAccessBase<Order>, IOrderDAO {
        public OrderDAO(PlatformAntiquesHandicraftsContext context) : base(context) {
        }

        public Order UpdateOrder(Order order) {
            Update(order);
            return Get(order.Id);
        }

        public Order Get(int id) {
            return GetAll().Include(p => p.OrderItems).FirstOrDefault(p => p.Id == id && p.Status == (int) Status.Available);
        }

        public IQueryable<Order> GetAllByBuyerId(int id) {
            return GetAll().Include(p => p.OrderItems).Where(p => p.BuyerId == id && p.Status == (int) Status.Available);
        }

        public IQueryable<Order> GetAllBySellerId(int id) {
            return GetAll().Include(p => p.OrderItems).Where(p => p.SellerId == id && p.Status == (int) Status.Available);
        }

        public IQueryable<Order> GetAllOrder() {
            return GetAll().Include(p => p.OrderItems).Where(p => p.Status == (int) Status.Available);
        }
    }
}
