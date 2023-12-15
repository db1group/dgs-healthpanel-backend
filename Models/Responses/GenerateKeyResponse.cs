using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class GenerateKeyResponse : IActionResult
    {
        public Guid Key { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
                => await new JsonResult(this).ExecuteResultAsync(context);
    }
}
