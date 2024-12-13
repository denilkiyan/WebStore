using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using WebStoreApp.Application.DTOModels;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Application.Services;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly UserService _userService;
        private readonly CartService _cartService;
        public CartController(ProductService productService, UserService userService, CartService cartService)
        {
            _productService = productService;
            _userService = userService;
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            User user = await _userService.GetByEmail(HttpContext.User.Identity.Name);
            if (user == null) return Unauthorized(new { Message = "User not exist" });

            var carts = await _cartService.GetCart(user.Id);
            if (carts == null) return NotFound(new { Message = "Cart not exist" });
            else return Ok(new { Products = carts});
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProductToCart([FromBody] ProductCartRequest request)
        {
            User user = await _userService.GetByEmail(HttpContext.User.Identity.Name);
            if (user == null) return Unauthorized(new { Message = "User not exist" });

            var CartItems = await _cartService.AddProductToCart(user, request.id, request.quantity);
            if (!CartItems.IsSuccess) return NotFound(CartItems.Message);
            else return Ok(new { Message = CartItems.Message, Id = request.id});
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductFromCart(int id)
        {
            User user = await _userService.GetByEmail(HttpContext.User.Identity.Name);
            if (user == null) return Unauthorized(new { Message = "User not exist" });

            var CartItems = await _cartService.DeleteProductFromCart(user.Id, id);
            if (!CartItems.IsSuccess) return NotFound(new { Message = CartItems.Message });
            else return Ok(new { Message = CartItems.Message });
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangeCart([FromBody] ProductCartRequest request)
        {
            User user = await _userService.GetByEmail(HttpContext.User.Identity.Name);
            if (user == null) return Unauthorized(new { Message = "User not exist" });

            var CartItems = await _cartService.ChangeProductInCart(user.Id, request.id, request.quantity);
            if (!CartItems.IsSuccess) return NotFound(new { Message = CartItems.Message });
            else return Ok(new { Message = CartItems.Message });
        }
    }
}
