using Db1HealthPanelBack.Infra.Http;

namespace Db1HealthPanelBack.Configs
{
    public static class IntegrationConfig
    {
        public static IServiceCollection AddIntegrations(this IServiceCollection services)
        {
            services.AddHttpClient<TechRadarHttpService>();

            return services;
        }
    }
}
