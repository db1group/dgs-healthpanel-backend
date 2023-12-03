using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TechRadarController(TechRadarService techRadarService) : ControllerBase
    {
        private readonly TechRadarService _techRadarService = techRadarService;

        [HttpGet("compare")]
        public async Task<IActionResult> GetRadarTechComparison([FromQuery] List<Guid>? projectIds)
        {
            var result = await _techRadarService.FetchTechRadarComparison(projectIds);

            return result is null ? UnprocessableEntity() : Ok(result);
        }
    }
}
