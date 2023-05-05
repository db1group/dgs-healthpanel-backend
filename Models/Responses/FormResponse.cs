using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class FormResponse : IActionResult
    {
        public ICollection<PillarResponse>? Pillars { get; set; }
        public DateTime? AccrualMonth { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
            => await new JsonResult(this).ExecuteResultAsync(context);
    }
}