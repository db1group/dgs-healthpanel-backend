namespace Db1HealthPanelBack.Models.Responses
{
    public class EvaluationResponse
    {
        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public Guid CostCenterId { get; set; }
        public string? CostCenterName { get; set; }
        public decimal ProcessHealthScore { get; set; }
        public decimal MetricsHealthScore { get; set; }
        public DateTime Date { get; set; }
        public decimal HealthScore => (ProcessHealthScore + MetricsHealthScore) / 2;
    }
}