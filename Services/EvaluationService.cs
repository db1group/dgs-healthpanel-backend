using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Infra.Shared;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Services
{
    public class EvaluationService
    {
        private readonly ContextConfig _contextConfig;

        public EvaluationService(ContextConfig contextConfig)
        {
            _contextConfig = contextConfig;
        }

        public async Task<IEnumerable<EvaluationResponse>> GetEvaluationsAsync(IEnumerable<Guid> projectIds, IEnumerable<DateTime>? dates)
        {
            var datesFilter = dates ?? new List<DateTime>{ DateTime.Now };

            var result = await _contextConfig.Evaluations
                .Where(x => projectIds.ToList().Contains(x.ProjectId) && datesFilter.ToList().Contains(x.Date))
                .ToListAsync();

            return result.Adapt<List<EvaluationResponse>>();
        }
    }
}