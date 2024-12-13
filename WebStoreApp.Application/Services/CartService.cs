using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Application.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ProductService _productService;
        public CartService(ICartRepository cartRepository, ProductService productService)
        {
            _cartRepository = cartRepository;
            _productService = productService;

        }
        public async Task<IEnumerable<Cart>> GetCart(int userId)
        {
            var productsInCart = await _cartRepository.GetCartByUserIdAsync(userId);
            return productsInCart;
        }

        public async Task<(bool IsSuccess, string Message)> AddProductToCart(User user, int productId, int quantity) //!!!!!!ПЕРЕСМОТРЕТЬ МЕТОД 
        {
            var product = await _productService.GetProductById(productId);
            if (product == null) return (false, "Product not exist");

            var cart = await _cartRepository.GetCartAsync(user.Id, product.Id);
            if (cart != null)
            {
                cart.Quantity += quantity;
                await _cartRepository.UpdateCartAsync(cart);
            }
            else
            {
                Cart Cart = Cart.CreateCart(user, user.Id, product, product.Id, quantity);
                await _cartRepository.CreateCartAsync(Cart);
            }
            return (true, "Product added to cart");
        }

        public async Task<(bool IsSuccess, string Message)> DeleteProductFromCart(int userId, int productId)
        {
            var productInCart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (productInCart == null)
            {
                return (false, "Cart not exist");
            }

            Cart cart = productInCart.FirstOrDefault(c => c.ProductID == productId);
            if (cart == null) return (false, "Product not exist");
            else
            {
                await _cartRepository.DeleteCartAsync(cart);
                return (true, "Product was delete");
            }
        }

        public async Task<(bool IsSuccess, string Message)> ChangeProductInCart(int userId, int productId, int quantity)
        {
            var productInCart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (productInCart == null)
            {
                return (false, "Cart not exist");
            }

            Cart cart = productInCart.FirstOrDefault(c => c.ProductID == productId);
            if (cart == null) return (false, "Product not exist");
            else
            {
                cart.Quantity = quantity;
                await _cartRepository.UpdateCartAsync(cart);
                return (true, "Product was change");
            }
        }
    }
}
