using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Mapster;

namespace Db1HealthPanelBack.Services
{
    public class QualityGateService
    {
        private readonly ContextConfig _contextConfig;

        public QualityGateService(ContextConfig contextConfig)
        {
            _contextConfig = contextConfig;
        }

        public async Task<SonarQualityMetricsResponse> SyncAllMetrics(SonarQualityMetricsRequest sonarQualityMetricsRequest)
        {
            var remoteMetrics = sonarQualityMetricsRequest.SonarMetrics!.Adapt<ICollection<SonarMetric>>();

            foreach (var remoteMetric in remoteMetrics)
            {
                if (!_contextConfig.Exists(remoteMetric)) _contextConfig.Add(remoteMetric);
            }

            await _contextConfig.SaveChangesAsync();

            return new SonarQualityMetricsResponse { SincronizedMetrics = true };
        }
    }
}