using System.Security.Claims;

namespace Db1HealthPanelBack.Services
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public bool IsAuthenticated
            => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated
            ?? false;

        public string UserId
            => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.PrimarySid)
            ?? string.Empty;

        public string UserName
            => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)
            ?? string.Empty;

        public string UserEmail
            => _httpContextAccessor.HttpContext?.User?.Identity is ClaimsIdentity identity
                ? identity.Claims.ElementAt(11)?.Value ?? string.Empty : string.Empty;
    }
}