using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class AnswerResponse : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(null){ StatusCode = StatusCodes.Status201Created };

            await result.ExecuteResultAsync(context);
        }
    }
}