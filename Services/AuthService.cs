using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Db1HealthPanelBack.Infra.Shared;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Db1HealthPanelBack.Services
{
    public class AuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Login(LoginRequest user)
        {
            var userLoginResponse = await _signInManager.PasswordSignInAsync(
                user.Email,
                user.Password,
                isPersistent: false,
                lockoutOnFailure: false
             );
            if (!userLoginResponse.Succeeded)
                return new ErrorAuthResponse(ErrorMessage.LoginFail, StatusCodes.Status400BadRequest, null);

            return new AuthResponse(await GetCredentials(user.Email));
        }

        public async Task<IActionResult> LoginWithoutPassword(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ErrorAuthResponse(ErrorMessage.LoginFail, StatusCodes.Status401Unauthorized, null);

            return new AuthResponse(await GetCredentials(user.Email ?? string.Empty));
        }

        public async Task<TokenResponse> GetCredentials(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var accessTokenClaims = await GetClaims(user!, true);
            var refrashTokenClaims = await GetClaims(user!, false);

            var dataExperationAccessToken = DateTime.UtcNow.AddSeconds(
                double.Parse(_configuration["JwtOptions:AccessTokenExpiration"]!));
            var dataExperationRefreshToken = DateTime.UtcNow.AddSeconds(
                double.Parse(_configuration["JwtOptions:RefreshTokenExpiration"]!));

            var accessToken = GenerateToken(accessTokenClaims, dataExperationAccessToken);
            var refreshToken = GenerateToken(refrashTokenClaims, dataExperationRefreshToken);

            return new TokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        private string GenerateToken(IEnumerable<Claim> claims, DateTime expirationDate)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                claims: claims,
                expires: expirationDate,
                notBefore: DateTime.Now,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<IList<Claim>> GetClaims(IdentityUser user, bool addUserClaims)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user?.Id!),
                new(JwtRegisteredClaimNames.Email, user?.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
            };

            if (addUserClaims && user != null)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(userClaims);

                foreach (var role in roles)
                    claims.Add(new Claim("role", role));
            }

            return claims;
        }
    }
}
