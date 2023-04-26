using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class LeadResponse : IActionResult
    {

        public string? Name { get; set; }
        public string? ProjectName { get; set; }
        public Boolean? InTraining { get; set; }

        public LeadResponse(string? name, string? projectName, Boolean? inTraining)
        {
            this.Name = name;
            this.ProjectName = projectName;
            this.InTraining = inTraining;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new
            {
                Name = this.Name,
                ProjectName = this.ProjectName,
                InTraining = this.InTraining
            };

            await new JsonResult(result).ExecuteResultAsync(context);
        }
    }
}