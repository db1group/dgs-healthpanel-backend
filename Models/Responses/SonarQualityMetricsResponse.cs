using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class SonarQualityMetricsResponse : IActionResult
    {
        public bool SincronizedMetrics { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this);

            await result.ExecuteResultAsync(context);
        }
    }
}