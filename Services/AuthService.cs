using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Microsoft.AspNetCore.Mvc;
using Db1HealthPanelBack.Infra.Shared;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Db1HealthPanelBack.Infra.Http;

namespace Db1HealthPanelBack.Services
{
    public class AuthService
    {
        private readonly HttpService _httpService;
        private readonly IConfiguration _configuration;

        public AuthService(
            HttpService httpService,
            IConfiguration configuration
        )
        {
            _httpService = httpService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Login(LoginRequest user)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:8080/realms/db1-realm/protocol/openid-connect/token"),
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "client_id", "health-panel" },
                    { "client_secret", "nY0DMoN0nrA51IBdtEdobtT7WOxDtjBa" },
                    { "username", user.UserName },
                    { "password", user.Password },
                    {"Content-Type", "application/x-www-form-urlencoded"}
                })
            };

            var response = await _httpService.Post<TokenResponse>(request);

            if (response == null)
                return new ErrorAuthResponse(ErrorMessage.LoginFail, StatusCodes.Status401Unauthorized, null);

            return new AuthResponse(response!);
        }


        // public async Task<IActionResult> LoginWithoutPassword(string userId)
        // {
        //     // var user = await _userManager.FindByIdAsync(userId);

        //     var user = "null";
        //     if (user == null)
        //         return new ErrorAuthResponse(ErrorMessage.LoginFail, StatusCodes.Status401Unauthorized, null);

        //     return new AuthResponse(await GetCredentials(string.Empty));
        // }

        // public ChallengeResult ExternalLogin(string provider, string? returnURL = null)
        // {
        //     // var redirectURL = _urlHelper.Action("RegisterExternalUser", new { returnURL });
        //     var redirectURL = "teste-login";
        //     // var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectURL);
        //     // return new ChallengeResult(provider, properties);
        // }

    }
}
