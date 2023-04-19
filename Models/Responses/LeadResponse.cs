using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class LeadResponse : IActionResult
    {

        public string? Name { get; set; }
        public string? ProjectName { get; set; }

        public LeadResponse(string? name, string? projectName)
        {
            this.Name = name;
            this.ProjectName = projectName;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new
            {
                Name = this.Name,
                ProjectName = this.ProjectName
            };

            await new JsonResult(result).ExecuteResultAsync(context);
        }
    }
}