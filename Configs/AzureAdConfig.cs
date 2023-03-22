using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace Db1HealthPanelBack.Configs
{
    public static class AzureAdConfig
    {
        public static void AddAzureAdAuth(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options => 
                {
                    configurationManager.Bind("AzureAd", options);

                    options.TokenValidationParameters.NameClaimType = "name";
                },
                options => { configurationManager.Bind("AzureAd", options); });
        }
        
    }
}