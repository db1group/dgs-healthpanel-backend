using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class LeadProjectResponse : IActionResult
    {
        public Guid ProjectId { get; set; }
        public Guid LeadId { get; set; }
        public string? LeadName { get; set; }
        public string? ProjectName { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this);

            await result.ExecuteResultAsync(context);
        }
    }
}