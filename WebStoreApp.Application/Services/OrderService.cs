using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Application.Services
{
    public class OrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderService(ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<Order>> GetOrders(int userId)
        {
            return await _orderRepository.GetOrdersByUserId(userId);
        }
        public async Task<decimal> CalculatePrice(IEnumerable<Cart> carts)
        {
            decimal result = 0;
            foreach (var product in carts)
            {
                result += product.Product.Price * product.Quantity;
            }
            return result;
        }

        public async Task ClearCart(IEnumerable<Cart> carts)
        {
            foreach (var cart in carts)
            {
                await _cartRepository.DeleteCartAsync(cart);
            }
        }

        public async Task<(bool IsSuccess, string Message)> AddOrder(User user)
        {
            var carts = await _cartRepository.GetCartByUserIdAsync(user.Id);
            if (!carts.Any()) return (false, "Cart not exist");

            await _orderRepository.CreateOrder(Order.CreateOrder(user, user.Id, await CalculatePrice(carts), "pending"));
            await ClearCart(carts);
            return (true, "Order was create");
        }
    }
}
