using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses.Errors
{
    public class ErrorResponse : IActionResult
    {
        public ErrorResponse(string message)
        {
            ErrorMessage = message;
        }
        
        public string? ErrorMessage { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this)
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}