using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class CostCenterResponse : IActionResult
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public async Task ExecuteResultAsync(ActionContext context)
            => await new JsonResult(this).ExecuteResultAsync(context);
    }
}