using System.Collections.Concurrent;
using System.Text;
using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Db1HealthPanelBack.Services
{
    public class EvaluationService
    {
        private readonly ContextConfig _contextConfig;
        private readonly IMemoryCache _cache;

        public EvaluationService(ContextConfig contextConfig, IMemoryCache cache)
        {
            _contextConfig = contextConfig;
            _cache = cache;
        }

        public async Task<IEnumerable<EvaluationResponse>> GetEvaluationsAsync(IEnumerable<Guid>? projectIds,
            IEnumerable<Guid>? costCenterIds, DateTime? startDate, DateTime? endDate)
        {
            var query = GetBaseQuery(projectIds, costCenterIds, startDate, endDate);

            var result = await query.ToListAsync();

            return await GroupEvaluationsByCostCenterAsync(result);
        }

        public async Task<IEnumerable<EvaluationAnalyticResponse>> GetEvaluationsAnalyticAsync(IEnumerable<Guid>? projectIds,
            IEnumerable<Guid>? costCenterIds, DateTime? startDate, DateTime? endDate)
        {
            var cacheKey = GenerateCacheKey(projectIds, costCenterIds, startDate, endDate);

            if (_cache.TryGetValue(cacheKey, out IEnumerable<EvaluationAnalyticResponse>? cachedResult))
                return cachedResult!;

            var query = GetBaseQuery(projectIds, costCenterIds, startDate, endDate)
                .Include(x => x.Answer)
                    .ThenInclude(x => x!.Pillars!)
                    .ThenInclude(x => x.Pillar);

            var evaluations = await query.ToListAsync();

            var evaluationsWithScores = new List<EvaluationAnalyticResponse>();

            foreach (var evaluation in evaluations)
            {
                var pillarsScores = await CalculateProcessScore(evaluation.AnswerId ?? Guid.Empty);

                var evaluationWithScores = new EvaluationAnalyticResponse
                {
                    CostCenterName = evaluation.Project?.CostCenter?.Name,
                    User = evaluation.Answer is not null ? evaluation.Answer.UserId.ToString() : null,
                    PillarScores = pillarsScores.Any() ? pillarsScores : new List<PillarScore>(),
                    Date = evaluation.Date,
                    MetricsHealthScore = Math.Round(evaluation.MetricsHealthScore, 2),
                    ProcessHealthScore = Math.Round(evaluation.ProcessHealthScore, 2),
                    ProjectName = evaluation.Project?.Name
                };

                evaluationsWithScores.Add(evaluationWithScores);
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
            _cache.Set(cacheKey, evaluationsWithScores, cacheOptions);

            return evaluationsWithScores;
        }


        public async Task FeedEvaluation(Guid projectId, decimal processHealthScore, Guid? answerId)
        {
            var evaluation = await _contextConfig.Evaluations
                                .FirstOrDefaultAsync(prop => prop.ProjectId == projectId
                                                        && prop.Date.Month == DateTime.Now.Month
                                                        && prop.Date.Year == DateTime.Now.Year);

            if (evaluation is not null)
                evaluation.ProcessHealthScore = processHealthScore;
            else evaluation = new Evaluation
            {
                Date = DateTime.Now,
                ProcessHealthScore = processHealthScore,
                ProjectId = projectId
            };

            evaluation.AnswerId = answerId;

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
            foreach (var evaluation in evaluations)
            {
                yield return new EvaluationResponse
                {
                    CostCenterId = costCenter.Id,
                    CostCenterName = costCenter.Name,
                    Date = evaluation.Date,
                    MetricsHealthScore = Math.Round(evaluations.Average(prop => prop.MetricsHealthScore), 2),
                    ProcessHealthScore = Math.Round(evaluations.Average(prop => prop.ProcessHealthScore), 2),
                    ProjectName = costCenter.Name
                };
            }
        }

        private IQueryable<Evaluation> GetBaseQuery(IEnumerable<Guid>? projectIds, IEnumerable<Guid>? costCenterIds, DateTime? startDate, DateTime? endDate)
        {
            var query = _contextConfig.Evaluations
                            .Include(p => p.Project)
                            .ThenInclude(p => p!.CostCenter)
                            .AsQueryable();

            if (projectIds is not null && projectIds.Any())
                query = query.Where(x => projectIds.ToList().Contains(x.ProjectId));

            if (costCenterIds is not null && costCenterIds.Any())
                query = query.Where(x => costCenterIds.ToList().Contains(x.Project!.CostCenterId));

            if (startDate is not null)
                query = query.Where(x => x.Date >= startDate);

            if (endDate is not null)
                query = query.Where(x => x.Date <= endDate);

            return query;
        }

        public async Task<IEnumerable<PillarScore>> CalculateProcessScore(Guid answerId)
        {
            var answerResult = answerId != Guid.Empty ? await _contextConfig.Answers
                .Include(prop => prop.Questions)
                .Include(prop => prop.Pillars!)
                    .ThenInclude(prop => prop.Pillar!)
                    .ThenInclude(prop => prop.Columns!)
                        .ThenInclude(prop => prop.Questions)
                .FirstAsync(prop => prop.Id == answerId) : null;

            var pillarsScore = new List<PillarScore>();

            if (answerResult is not null)
                foreach (var pillar in answerResult.Pillars!)
                {
                    var questionsScore = new List<dynamic>();

                    foreach (var column in pillar.Pillar!.Columns!)
                    {
                        var questionsIds = column.Questions!.Select(qt => qt.Id);

                        var answerQuestionsScore = answerResult.Questions!
                            .Where(prop => questionsIds.Contains(prop.QuestionId) && prop.Value == "DONE");

                        var percentageQuestion = decimal.Round((decimal)answerQuestionsScore.Count() / column.Questions!.Count * 100, 2);
                        var questionScore = new
                        {
                            column.Title,
                            PercentageQuestionScore = percentageQuestion,
                            ColumnScore = (percentageQuestion * column.Weight) / 100,
                            ColumnWeight = column.Weight,
                            column.PillarId,
                            PillarTitle = pillar.Pillar.Title,
                            PillarWeight = pillar.Pillar.Weight
                        };

                        questionsScore.Add(questionScore);
                    }

                    var tempCalc = decimal.Round(questionsScore
                        .Where(prop => prop.PillarId == pillar.PillarId)
                        .Sum(prop => (decimal)prop.ColumnScore), 2);

                    var percentagePillar = Math.Min(tempCalc, 100);
                    var pillarScore = percentagePillar * pillar.Pillar.Weight / 100;

                    pillarsScore.Add(new PillarScore
                    {
                        Name = pillar.Pillar.Title!,
                        Score = pillarScore ?? 0
                    });
                }

            return pillarsScore;
        }

        private string GenerateCacheKey(IEnumerable<Guid>? projectIds, IEnumerable<Guid>? costCenterIds, DateTime? startDate, DateTime? endDate)
        {
            var key = $"CacheKey_EvaluationsAnalytic_";

            if (projectIds != null)
                key += string.Join("_", projectIds) + "_";

            if (costCenterIds != null)
                key += string.Join("_", costCenterIds) + "_";

            if (startDate != null)
                key += startDate.Value.ToString("yyyyMMdd") + "_";

            if (endDate != null)
                key += endDate.Value.ToString("yyyyMMdd") + "_";

            return key;
        }
    }
}