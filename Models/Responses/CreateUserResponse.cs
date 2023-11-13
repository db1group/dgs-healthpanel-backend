using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class CreateUserResponse : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this)
            {
                StatusCode = StatusCodes.Status201Created
            };

            await result.ExecuteResultAsync(context);
        }
    }
}