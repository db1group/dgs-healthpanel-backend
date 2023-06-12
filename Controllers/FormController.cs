using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly FormService _formService;

        public FormController(FormService formService)
            =>  _formService = formService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "project")] Guid id)
            => await _formService.GetForm(id);

        [HttpPost]
        public async Task<IActionResult> CreateForm([FromBody] FormRequest form)
            => await _formService.CreateForm(form);
    }
}