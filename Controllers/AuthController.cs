using Db1HealthPanelBack.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        [HttpGet]
        public ActionResult<string> GetUser()
        {
            return "Teste de retorno GET";
        }

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = new IdentityUser
            {
                UserName = request.Name,
                Email = request.Email,
                EmailConfirmed = true
            };

            var userCreateResult = await _userManager.CreateAsync(user, request.Password);

            if (!userCreateResult.Succeeded)
            {
                return BadRequest(userCreateResult.Errors);
            }

            await _signInManager.SignInAsync(user, false);

            return Ok();
        }

    }

}
