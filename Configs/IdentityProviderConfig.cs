using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace Db1HealthPanelBack.Configs
{
    public static class IdentityProviderConfig
    {
        public static void AddKeyCloakAuth(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var authenticationOptions = configurationManager.GetSection("IdentityProviders:KeyCloak").Get<KeycloakAuthenticationOptions>();

            services.AddKeycloakAuthentication(authenticationOptions!);
        }
    }
}