using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Configs
{
    public static class EntityConfig
    {
        public static void AddDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContextFactory<ContextConfig>(options =>
            {
                options.UseNpgsql(configurationManager.GetConnectionString("Database"));
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });
        }
    }
}