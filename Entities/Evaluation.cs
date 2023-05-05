namespace Db1HealthPanelBack.Entities
{
    public sealed class Evaluation
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }
        public decimal ProcessHealthScore { get; set; }
        public decimal MetricsHealthScore { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal HealthScore => (ProcessHealthScore + MetricsHealthScore) / 2;
    }
}