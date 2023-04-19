using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly LeadService _leadService;

        public LeadController(LeadService leadService)
        {
            _leadService = leadService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeadEngineer([FromBody] LeadRequest leadRequest)
            => await _leadService.CreateLeadEngineer(leadRequest);
    }
}