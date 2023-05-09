using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class ProjectResponse : IActionResult
    {
        private int StatusCode { get; set; } = 200;
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<LeadProjectResponse>? LeadProjects { get; set; }
        public CostCenterResponse? CostCenter { get; set; }

        public void SetStatusCode(int statusCode)
            => StatusCode = statusCode;

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this) { StatusCode = StatusCode };

            await result.ExecuteResultAsync(context);
        }
    }
}