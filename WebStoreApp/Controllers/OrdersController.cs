using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStoreApp.Application.Services;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly OrderService _orderService;
        public OrdersController(UserService userService, OrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            User user = await _userService.GetByEmail(HttpContext.User.Identity.Name);
            if (user == null) return Unauthorized(new { Message = "User not exist" });

            var orders = await _orderService.GetOrders(user.Id);
            if (!orders.Any()) return NotFound(new { Message = "Orders not exist" });
            else return Ok(new { Orders = orders });
        }
    }
}
