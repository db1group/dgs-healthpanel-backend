using Db1HealthPanelBack.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class AuthResponse : IActionResult
    {
        private TokenResponse userToken;

        public AuthResponse(TokenResponse userToken)
        {
            this.userToken = userToken;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(userToken);

            await result.ExecuteResultAsync(context);
        }
    }
}