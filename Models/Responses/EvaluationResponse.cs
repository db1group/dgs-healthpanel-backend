using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class EvaluationResponse : IActionResult
    {
        public EvaluationResponse(Guid projectId, decimal processHealthScore, decimal metricsHealthScore, DateTime date)
        {
            ProjectId = projectId;
            ProcessHealthScore = processHealthScore;
            MetricsHealthScore = metricsHealthScore;
            Date = date;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
                var result = new
            {
                Name = this.ProjectId,
                ProjectName = this.ProcessHealthScore,
                InTraining = this.MetricsHealthScore,
                Date = this.Date
            };

            await new JsonResult(result).ExecuteResultAsync(context);
        }

        public Guid ProjectId { get; set; }
        public decimal ProcessHealthScore { get; set; }
        public decimal MetricsHealthScore { get; set; }
        public DateTime Date { get; set; }
        public decimal HealthScore => (ProcessHealthScore + MetricsHealthScore / 2);
    }
}