namespace Db1HealthPanelBack.Models.Requests
{
    public class SonarQualityMetricsRequest
    {
        public ICollection<SonarMetricRequest>? SonarMetrics { get; set; }
    }
}