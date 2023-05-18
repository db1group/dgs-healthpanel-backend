using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.ResponseCompression;

namespace Db1HealthPanelBack.Configs
{
    public static class ServicesConfig
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<FormService>();
            services.AddScoped<LeadService>();
            services.AddScoped<EvaluationService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<CostCenterService>();
            services.AddScoped<CurrentUserService>();
        }

        public static void AddCompressionToResponse(this IServiceCollection services)
            => services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = new[]
            {
                "text/plain",
                "text/css",
                "application/javascript",
                "text/html",
                "application/xml",
                "text/xml",
                "application/json",
                "text/json"
            };
        });
    }
}