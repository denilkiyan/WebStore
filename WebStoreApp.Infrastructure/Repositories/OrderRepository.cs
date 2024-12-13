using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Domain.Models;
using WebStoreApp.Infrastructure.DBContext;

namespace WebStoreApp.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _storeContext;
        public OrderRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task CreateOrder(Order order)
        {
            await _storeContext.Orders.AddAsync(order);
            await _storeContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            var orders = (from c in _storeContext.Orders
                          where c.UserId == userId
                          select c).ToList();
            await _storeContext.SaveChangesAsync();
            return orders;
        }
    }
}
