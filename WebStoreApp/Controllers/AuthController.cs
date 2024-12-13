using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStoreApp.Application.DTOModels;
using WebStoreApp.Application.Services;

namespace WebStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(new { Message = "Data is not valid" });
            else
            {
                await _userService.Register(request.Email, request.UserName, request.Password);
                return Ok(new { Message = "Registration successful" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(new { Message = "Data is not valid" });
            var result = await _userService.Login(request.Email, request.Password);
            if (!result.IsSuccess)
            {
                return Unauthorized(new { Message = result.ErrorMessage });
            }
            else return Ok(new { Token=result.Token });
        }
    }
}
