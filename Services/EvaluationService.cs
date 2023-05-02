using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Models.Responses;
using Mapster;
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


        public async Task<IEnumerable<EvaluationResponse>> GetEvaluationsAsync(IEnumerable<Guid> projectIds, DateTime? startDate, DateTime? endDate)
        {
            var startDateFilter = startDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var query = _contextConfig.Evaluations
                            .Where(x => projectIds.ToList().Contains(x.ProjectId) &&
                                    x.Date >= startDateFilter);

            if (endDate is not null) {
                query = query.Where(x => x.Date <= endDate);
            }

            var result = await query.ToListAsync();

            return result.Adapt<IEnumerable<EvaluationResponse>>();
        }
    }
}