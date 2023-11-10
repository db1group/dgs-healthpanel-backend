using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Db1HealthPanelBack.Configs
{
    public static class AuthConfig
    {
        public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configurationManager)
            => services.AddAuthentication("JwtBearer")
                       .AddJwtBearer("JwtBearer", options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidIssuer = configurationManager["JwtOptions:Issuer"],

                                ValidateAudience = true,
                                ValidAudience = configurationManager["JwtOptions:Audience"],

                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager["JwtOptions:Key"] ?? "")),

                                ValidateLifetime = true,
                            };
                        });
    }
}