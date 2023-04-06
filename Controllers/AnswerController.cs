using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly FormService _formService;

        public AnswerController(FormService formService)
        {
            _formService = formService;
        }

        [HttpPut]
        public async Task<IActionResult> Submit([FromBody] AnswerRequest request)
            => await _formService.SubmitAnswer(request);
    }
}