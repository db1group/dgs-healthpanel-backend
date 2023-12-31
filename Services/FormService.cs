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
        private readonly CurrentUserService _currentUserService;
        private readonly MetricsHealthScoreService _metricsHealthScoreService;

        public FormService(ContextConfig contextConfig, EvaluationService evaluationService,
            CurrentUserService currentUserService, MetricsHealthScoreService metricsHealthScoreService)
        {
            _contextConfig = contextConfig;
            _evaluationService = evaluationService;
            _currentUserService = currentUserService;
            _metricsHealthScoreService = metricsHealthScoreService;
        }

        public async Task<IActionResult> GetForm(Guid id)
        {
            var project = await _contextConfig.Projects
                            .FirstOrDefaultAsync(prop => prop.Id == id);

            if (project is null) return new ErrorResponse("Project Not Found");

            var result = await _contextConfig.Pillars
                            .Include(prop => prop.Columns!)
                            .ThenInclude(p => p.Questions)
                            .ToListAsync();

            var answerFetched = await _contextConfig.Answers
                                    .WithProject(id)
                                    .FetchWithQuestionAndPillars()
                                    .FetchWithMonthRange()
                                    .OrderByDescending(prop => prop.CreatedAt)
                                    .FirstOrDefaultAsync();

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
                    Pillar = _contextConfig.Pillars.Include(pillar => pillar.Columns!)
                                                        .ThenInclude(column => column.Questions)
                                                   .First(prop => prop.Id == p),
                    PillarId = p ?? Guid.NewGuid(),
                    AdditionalData = request.Pillars?.First(rp => rp.PillarId == p).AdditionalData
                }).ToList(),
                UserId = await GetUserId(project.Id)
            };

            await _contextConfig.AddRangeAsync(newAnswers);
            await _contextConfig.SaveChangesAsync();

            var processScoreCalculated = _evaluationService.CalculateProcessScore(newAnswers);
            var metricsHealthScoreCalculated = await _metricsHealthScoreService.GetMetricsHealthScore(project);
            await _evaluationService.FeedEvaluation(newAnswers.ProjectId,
                processScoreCalculated.Sum(prop => prop.Score), metricsHealthScoreCalculated, newAnswers!.Id);

            return new AnswerResponse();
        }

        private async Task<Guid> GetUserId(Guid projectId)
        {
            var responder =
                await _contextConfig.ProjectResponders
                    .FirstOrDefaultAsync(prop => prop.Email == _currentUserService.UserName &&
                                                 prop.ProjectId == projectId);
            return responder?.Id ?? Guid.Empty;
        }
    }
}