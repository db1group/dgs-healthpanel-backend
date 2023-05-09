using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly EvaluationService _evaluationService;

        public EvaluationController(EvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        [HttpGet]
        public async Task<IEnumerable<EvaluationResponse>> Get([FromQuery(Name = "projectIds[]")] IEnumerable<Guid>? projectIds, [FromQuery(Name = "costCenterIds[]")] IEnumerable<Guid>? costCenterIds,
                                                                    DateTime? startDate, DateTime? endDate)
        {
            return await _evaluationService.GetEvaluationsAsync(projectIds, costCenterIds, startDate, endDate);
        }
    }
}