using System.Collections.Concurrent;
using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Models.Responses;
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

        public async Task<IEnumerable<EvaluationResponse>> GetEvaluationsAsync(IEnumerable<Guid>? projectIds,
            IEnumerable<Guid>? costCenterIds, DateTime? startDate, DateTime? endDate)
        {
            var query = _contextConfig.Evaluations
                .Include(p => p.Project)
                .ThenInclude(p => p!.CostCenter)
                .AsQueryable();

            if(projectIds is not null && projectIds.Any())
                query = query.Where(x => projectIds.ToList().Contains(x.ProjectId));

            if(costCenterIds is not null && costCenterIds.Any())
                query = query.Where(x => costCenterIds.ToList().Contains(x.Project!.CostCenterId));                

            if (startDate is not null)
                query = query.Where(x => x.Date >= startDate);

            if (endDate is not null)
                query = query.Where(x => x.Date <= endDate);

            var result = await query.ToListAsync();

            return await GroupEvaluationsByCostCenterAsync(result);
        }

        public async Task FeedEvaluation(Guid projectId, decimal processHealthScore)
        {
            var evaluation = await _contextConfig.Evaluations
                                .FirstOrDefaultAsync(prop => prop.ProjectId == projectId 
                                                        && prop.Date.Month == DateTime.Now.Month 
                                                        && prop.Date.Year == DateTime.Now.Year);
            
            if(evaluation is not null)
                evaluation.ProcessHealthScore = processHealthScore;
            else evaluation = new Evaluation
            {
                Date = DateTime.Now,
                ProcessHealthScore = processHealthScore,
                ProjectId = projectId
            };

            _contextConfig.Evaluations.Update(evaluation);
            await _contextConfig.SaveChangesAsync();
        }

        private async Task<IEnumerable<EvaluationResponse>> GroupEvaluationsByCostCenterAsync(IEnumerable<Evaluation> evalResult)
        {
            var costCenters = await _contextConfig.CostCenters.ToListAsync();
            var evaluations = new ConcurrentBag<EvaluationResponse>();

            await Task.WhenAll(costCenters.Select(async costCenter =>
            {
                var whileStartDate = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1);

                while (whileStartDate < DateTime.Now)
                {
                    var evaluationsWithSameCostCenter = evalResult.Where(prop =>
                        prop.Project!.CostCenterId == costCenter.Id &&
                        prop.Date.Year == whileStartDate.Year &&
                        prop.Date.Month == whileStartDate.Month);

                    if (evaluationsWithSameCostCenter.Any())
                    {
                        var responses = await Task.Run(() => CreateEvaluationResponses(evaluationsWithSameCostCenter, costCenter));

                        foreach (var response in responses)
                            evaluations.Add(response);
                    }

                    whileStartDate = whileStartDate.AddMonths(1);
                }
            }));

            return evaluations;
        }

        private IEnumerable<EvaluationResponse> CreateEvaluationResponses(IEnumerable<Evaluation> evaluations,
            CostCenter costCenter)
        {
            var evaluationResponses = new List<EvaluationResponse>();

            foreach (var evaluation in evaluations)
            {
                var evaluationResponse = new EvaluationResponse
                {
                    CostCenterId = costCenter.Id,
                    CostCenterName = costCenter.Name,
                    Date = evaluation.Date,
                    MetricsHealthScore = evaluations.Average(prop => prop.MetricsHealthScore),
                    ProcessHealthScore = evaluations.Average(prop => prop.ProcessHealthScore),
                    ProjectName = costCenter.Name
                };

                evaluationResponses.Add(evaluationResponse);
            }

            return evaluationResponses;
        }
    }
}