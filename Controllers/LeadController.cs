using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly LeadService _leadService;

        public LeadController(LeadService leadService)
        {
            _leadService = leadService;
        }

        [HttpGet]
        public async Task<IEnumerable<LeadResponse>> GetAll()
            => await _leadService.GetAllLeads();

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
            => await _leadService.FindLead(id);


        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] LeadRequest lead)
            => await _leadService.UpdateLead(id, lead);

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
            => await _leadService.DeleteLead(id);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeadRequest leadRequest)
            => await _leadService.CreateLead(leadRequest);

    }
}