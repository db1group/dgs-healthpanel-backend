namespace Db1HealthPanelBack.Entities
{
    public class SonarReadingDetail
    {
        public int Id { get; set; }
        public string? MetricKey { get; set; }
        public long ReadingId { get; set; }
        public string? Value { get; set; }
    }
}