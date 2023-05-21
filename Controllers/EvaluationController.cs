using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly EvaluationService _evaluationService;

        public EvaluationController(EvaluationService evaluationService)
            => _evaluationService = evaluationService;

        [HttpGet]
        public async Task<IEnumerable<EvaluationResponse>> Get([FromQuery(Name = "projectIds[]")] IEnumerable<Guid>? projectIds,
            [FromQuery(Name = "costCenterIds[]")] IEnumerable<Guid>? costCenterIds, DateTime? startDate, DateTime? endDate)
                => await _evaluationService.GetEvaluationsAsync(projectIds, costCenterIds, startDate, endDate);
    }
}