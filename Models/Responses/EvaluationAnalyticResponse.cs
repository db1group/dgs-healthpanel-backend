namespace Db1HealthPanelBack.Models.Responses
{
    public class EvaluationAnalyticResponse
    {
        public string? ProjectName { get; set; }
        public string? CostCenterName { get; set; }
        public decimal ProcessHealthScore { get; set; }
        public decimal MetricsHealthScore { get; set; }
        public DateTime Date { get; set; }
        public string? User {get;set;}
        public IEnumerable<PillarScore>? PillarScores {get;set;}
        public decimal HealthScore => (ProcessHealthScore + MetricsHealthScore) / 2;
    }

    public class PillarScore {
        public string? Name{get;set;}
        public decimal Score{get;set;}
    }
}