using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual User User { get; set; } = null!;
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = null!;
        public static Order CreateOrder(User user, int userId, decimal totalPrice, string status)
        {
            Order order = new();
            order.UserId = userId;
            order.User = user;
            order.TotalPrice = totalPrice;
            order.Status = status;
            return order;
        }
    }
}
