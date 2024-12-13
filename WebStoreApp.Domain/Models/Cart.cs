using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Domain.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public virtual User User { get; set; } = null!;
        public int UserID { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public static Cart CreateCart(User user, int userId, Product product, int productId, int quantity)
        {
            Cart cart = new Cart();
            cart.User = user;
            cart.UserID = userId;
            cart.Product = product;
            cart.ProductID = productId;
            cart.Quantity = quantity;
            return cart;
        }
    }
}
