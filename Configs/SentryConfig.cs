using Sentry;

namespace Db1HealthPanelBack.Configs
{
    public static class SentryConfig
    {
        public static void AddSentry(ConfigurationManager configurationManager)
            => SentrySdk.Init(options =>
                    {
                        options.Dsn = configurationManager.GetSection("Sentry").GetValue<string>("Dns");
                        options.Debug = true;
                        options.AutoSessionTracking = true;
                        options.IsGlobalModeEnabled = true;
                        options.EnableTracing = true;
                        options.Environment = configurationManager.GetSection("Sentry").GetValue<string>("Environment");
                    });
    }
}