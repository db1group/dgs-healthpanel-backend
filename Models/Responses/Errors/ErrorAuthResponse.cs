using System.Globalization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses.Errors
{
    public class ErrorAuthResponse : IActionResult
    {
        public ErrorAuthResponse(string message, int status, IEnumerable<IdentityError>? error)
        {
            ErrorMessage = message;

            StatusCode = status;

            Error = error;
        }

        public string? ErrorMessage { get; set; }
        public int? StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<IdentityError>? Error { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this)
            {
                StatusCode = StatusCode
            }; ;

            await objectResult.ExecuteResultAsync(context);
        }
    }
}