using Db1HealthPanelBack.Services;

namespace Db1HealthPanelBack.Configs
{
    public static class ServicesConfig
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<FormService>();
            services.AddTransient<LeadService>();
            services.AddTransient<EvaluationService>();
            services.AddTransient<ProjectService>();
        }
    }
}