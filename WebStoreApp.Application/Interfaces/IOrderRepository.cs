using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
    }
}
