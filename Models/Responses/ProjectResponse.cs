using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class ProjectResponse : IActionResult
    {
        public ProjectResponse(int statusCode = 200)
        {
            StatusCode = statusCode;
        }

        private int StatusCode { get; set; }
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public void SetStatusCode(int statusCode)
            => StatusCode = statusCode;

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this) { StatusCode = StatusCode};

            await result.ExecuteResultAsync(context);
        }
    }
}