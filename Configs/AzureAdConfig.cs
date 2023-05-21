using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace Db1HealthPanelBack.Configs
{
    public static class AzureAdConfig
    {
        public static void AddAzureAdAuth(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configurationManager.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddInMemoryTokenCaches();
        }
    }
}