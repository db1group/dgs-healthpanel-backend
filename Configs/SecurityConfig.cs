using System.Text;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Db1HealthPanelBack.Configs
{
    public static class SecurityConfig
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddKeycloakAuthentication(configurationManager);
            services.AddKeycloakAuthorization(configurationManager);

            return services;
        }

    }
}