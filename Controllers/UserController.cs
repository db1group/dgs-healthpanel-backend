using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
            => _userService = userService;

        [HttpPost("create-user")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
            => await _userService.CreateUser(request);
    }

}
