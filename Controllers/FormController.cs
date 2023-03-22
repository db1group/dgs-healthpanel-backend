using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly FormService _formService;

        public FormController(FormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        public async Task<ActionResult<FormResponse>> Get()
        {
            var response = await _formService.GetForm();

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<FormResponse>> CreateForm([FromBody] FormRequest form)
            => await _formService.CreateForm(form);
    }
}