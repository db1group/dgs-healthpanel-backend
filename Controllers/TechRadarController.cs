using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TechRadarController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly TechRadarService _techRadarService;
        private readonly StackService _stackService;

        public TechRadarController(IHttpClientFactory httpClientFactory, StackService stackService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _techRadarService = new TechRadarService();
            _stackService = stackService;
        }
        
        [HttpGet("compare")]
        public async Task<IActionResult> GetRadarTechComparison([FromQuery] List<Guid>? projectIds)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://techradar.db1.com.br/db1-opinion.json");
            if (!response.IsSuccessStatusCode) return StatusCode((int)response.StatusCode);
            
            var stackData = await _stackService.GetStacks(projectIds);
            string techRadarContent = await response.Content.ReadAsStringAsync();

            var techComparison = _techRadarService.GetTechComparisons(stackData, techRadarContent);

            return Ok(techComparison);

        }
    }
}
