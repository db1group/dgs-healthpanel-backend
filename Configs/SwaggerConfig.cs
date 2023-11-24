using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Db1HealthPanelBack.Configs
{
    public static class SwaggerConfig
    {
        public static void AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Health Panel API",
                    Version = "v1",
                    Description = "A .NET Web API for managing the Health Panel",
                    Contact = new OpenApiContact
                    {
                        Name = "Engineering Team",
                        Email = "engineeringteam@db1.com.br"
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
    }
}
