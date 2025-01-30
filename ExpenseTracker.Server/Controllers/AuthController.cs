using ExpenseTracker.Server.Models;
using ExpenseTracker.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var response = _authService.Register(request);
            if (response.Message == "Email already exists!") return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            var response = _authService.Login(request);
            if (response.Token == null) return Unauthorized(response);
            return Ok(response);
        }
    }
}
