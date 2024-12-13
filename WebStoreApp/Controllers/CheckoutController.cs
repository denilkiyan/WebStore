using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebStoreApp.Application.Services;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly OrderService _orderService;
        public CheckoutController(UserService userService,OrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            User user = await _userService.GetByEmail(HttpContext.User.Identity.Name);
            if (user == null) return Unauthorized(new { Message = "User not exist" });

            var orderStatus = await _orderService.AddOrder(user);
            if (!orderStatus.IsSuccess) return NotFound(new { Message = orderStatus.Message });
            else return Ok(new { Message = orderStatus.Message });
        }
    }
}
