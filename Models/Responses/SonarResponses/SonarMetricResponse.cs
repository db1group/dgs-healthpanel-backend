using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses.SonarResponses
{
    public class SonarMetricResponse : IActionResult
    {
        public string? Key { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Domain { get; set; }
        public string? Type { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this);

            await result.ExecuteResultAsync(context);
        }
    }
}