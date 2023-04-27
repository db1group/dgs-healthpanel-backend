using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
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

        [HttpGet("")]
        public async Task<IEnumerable<LeadResponse>> GetAllLeads()
            => await _leadService.GetAllLeads();

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
            => await _leadService.FindLead(id);



        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
            => await _leadService.DeleteLead(id);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeadRequest leadRequest)
            => await _leadService.CreateLead(leadRequest);

    }
}