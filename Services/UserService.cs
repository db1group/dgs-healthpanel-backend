using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Db1HealthPanelBack.Infra.Shared;

namespace Db1HealthPanelBack.Services
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> CreateUser(CreateUserRequest user)
        {
            var userData = new IdentityUser
            {
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = true
            };

            var userCreateResult = await _userManager.CreateAsync(userData, user.Password);
            if (!userCreateResult.Succeeded)
                return new ErrorAuthResponse(ErrorMessage.CreateUserFail, StatusCodes.Status400BadRequest, userCreateResult.Errors);

            await _signInManager.SignInAsync(userData, false);

            return new CreateUserResponse();
        }
    }
}
