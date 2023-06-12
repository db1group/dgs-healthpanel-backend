using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class QualityGateController : ControllerBase
    {
        private readonly QualityGateService _qualityGateService;

        public QualityGateController(QualityGateService qualityGateService)
        {
            _qualityGateService = qualityGateService;
        }

        [HttpPost]
        public async Task<IActionResult> SyncMetrics([FromBody] SonarQualityMetricsRequest sonarQualityMetricsRequest)
            => await _qualityGateService.SyncAllMetrics(sonarQualityMetricsRequest);

        [HttpGet("sonar-metrics")]
        public async Task<IActionResult> GetMetrics()
            => Ok(await _qualityGateService.GetAllSonarMetrics());
    }
}