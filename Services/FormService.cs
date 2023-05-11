using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Infra.Queries;
using Db1HealthPanelBack.Infra.Shared;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Services
{
    public class FormService
    {
        private readonly ContextConfig _contextConfig;
        private readonly EvaluationService _evaluationService;

        public FormService(ContextConfig contextConfig, EvaluationService evaluationService)
        {
            _contextConfig = contextConfig;
            _evaluationService = evaluationService;
        }

        public async Task<IActionResult> GetForm(Guid id)
        {
            var project = await _contextConfig.Projects
                                .FirstOrDefaultAsync(prop => prop.Id == id);

            if (project is null)
                return new ErrorResponse("Project Not Found");

            var pillarsQuery = _contextConfig.Pillars
                                    .Include(prop => prop.Columns!)
                                    .ThenInclude(p => p.Questions);

            var resultTask = pillarsQuery.ToListAsync();

            var answerFetchedTask = _contextConfig.Answers
                                        .WithProject(id)
                                        .FetchWithQuestionAndPillars()
                                        .FetchWithMonthRange()
                                        .OrderByDescending(prop => prop.CreatedAt)
                                        .FirstOrDefaultAsync();

            await Task.WhenAll(resultTask, answerFetchedTask);

            var result = resultTask.Result;
            var answerFetched = answerFetchedTask.Result;

            var formResponse = new FormResponse { Pillars = result.Adapt<ICollection<PillarResponse>>() };

            if (answerFetched is not null)
            {
                formResponse.AccrualMonth = answerFetched.CreatedAt.AddMonths(-1);
                formResponse.Pillars = formResponse.Pillars
                                            .Select(prop =>
                                            {
                                                var pillarFetched = answerFetched?.Pillars?
                                                                        .First(ap => ap.PillarId == prop.Id);

                                                prop.AdditionalData = pillarFetched?.AdditionalData;
                                                prop.Columns = prop.Columns?.Select(pc =>
                                                {
                                                    pc.Questions = pc.Questions?.Select(pq =>
                                                    {
                                                        var questionFetched = answerFetched?.Questions?
                                                                        .First(aq => aq.QuestionId == pq.Id);

                                                        pq.Value = questionFetched?.Value;

                                                        return pq;
                                                    }).ToList();

                                                    return pc;
                                                }).ToList();

                                                return prop;
                                            }).ToList();
            }

            return formResponse;
        }

        public async Task<IActionResult> CreateForm(FormRequest form)
        {
            var pillars = form.Pillars?.Adapt<IEnumerable<Pillar>>();

            if (pillars is not null)
            {
                await _contextConfig.Pillars.AddRangeAsync(pillars);
                await _contextConfig.SaveChangesAsync();
            }

            return new FormResponse
            {
                Pillars = pillars?.Adapt<ICollection<PillarResponse>>()
            };
        }

        public async Task<IActionResult> SubmitAnswer(AnswerRequest request)
        {
            var project = await _contextConfig.Projects.FirstOrDefaultAsync(p
                => p.Id == request.Project);

            if (project is null) return new ErrorResponse(ErrorMessage.ProjectNotFound);

            var answersRequestedIds = request.Questions?.Select(q => q.QuestionId);
            var answerPillarsIds = request.Pillars?.Select(p => p.PillarId);

            var newAnswers = new Answer
            {
                ProjectId = project.Id,
                Questions = answersRequestedIds?.Select(p => new AnswerQuestion
                {
                    QuestionId = p ?? Guid.NewGuid(),
                    Value = request.Questions?.First(rp => rp.QuestionId == p).Value
                }).ToList(),
                Pillars = answerPillarsIds?.Select(p => new AnswerPillar
                {
                    PillarId = p ?? Guid.NewGuid(),
                    AdditionalData = request.Pillars?.First(rp => rp.PillarId == p).AdditionalData
                }).ToList()
            };

            await _contextConfig.AddRangeAsync(newAnswers);
            await _contextConfig.SaveChangesAsync();

            await _evaluationService.FeedEvaluation(newAnswers.ProjectId, await CalculateProcessScore(newAnswers.Id));

            return new AnswerResponse();
        }

        private async Task<decimal> CalculateProcessScore(Guid answerId)
        {
            var answerResult = await _contextConfig.Answers
                .Include(prop => prop.Questions)
                .Include(prop => prop.Pillars!)
                .ThenInclude(prop => prop.Pillar!)
                .ThenInclude(prop => prop.Columns!)
                .ThenInclude(prop => prop.Questions)
                .FirstAsync(prop => prop.Id == answerId);

            var pillarsScore = new List<decimal>();

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

                pillarsScore.Add(pillarScore ?? 0);
            }

            return pillarsScore.Sum();
        }
    }
}