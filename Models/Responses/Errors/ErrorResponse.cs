using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses.Errors
{
    public class ErrorResponse : IActionResult
    {
        public ErrorResponse(string message)
        {
            ErrorMesssage = message;
        }
        
        public string? ErrorMesssage { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = 422;

            await ExecuteResultAsync(context);
        }
    }
}