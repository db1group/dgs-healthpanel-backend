using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly FormService _formService;

        public AnswerController(FormService formService)
            => _formService = formService;

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] AnswerRequest request)
            => await _formService.SubmitAnswer(request);
    }
}