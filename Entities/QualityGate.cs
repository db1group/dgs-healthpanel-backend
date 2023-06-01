namespace Db1HealthPanelBack.Entities
{
    public class QualityGate
    {
        public Guid Id { get; set; }
        public string? MetricName { get; set; }
        public string? MetricClassification { get; set; }
        public DateTime ScanDate { get; set; }
        public string? ProjectKey { get; set; }
    }
}