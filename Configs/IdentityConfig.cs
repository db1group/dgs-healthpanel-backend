using Microsoft.AspNetCore.Identity;

namespace Db1HealthPanelBack.Configs
{
    public static class IdentityConfig
    {
        public static void AddIdentity(this IServiceCollection services, ConfigurationManager configurationManager)
            => services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ContextConfig>()
                .AddDefaultTokenProviders();
    }
}
