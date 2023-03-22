using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
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
    }
}