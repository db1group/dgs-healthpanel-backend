using System.Security.Claims;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
            => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
         => await _authService.Login(request);

        [Authorize(AuthenticationSchemes = "JwtBearer")]
        [HttpPost("refresh-login")]
        public async Task<IActionResult> RefreshLogin()
        {
            var Identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return BadRequest();

            return await _authService.LoginWithoutPassword(userId);
        }
    }
}
