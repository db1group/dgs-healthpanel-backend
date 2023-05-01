using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Infra.Queries;
using Db1HealthPanelBack.Infra.Shared;
using Db1HealthPanelBack.Infra.Shared.Extension;
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

        public FormService(ContextConfig contextConfig)
        {
            _contextConfig = contextConfig;
        }

        public async Task<FormResponse> GetForm()
        {
            var result = await _contextConfig.Pillars
                .Include("Columns.Questions").ToListAsync();

            var answerFetched = await _contextConfig.Answers
                                    .FetchWithQuestionAndPillars()
                                    .FetchWithMonthRange()
                                    .FirstOrDefaultAsync();

            var formResponse = new FormResponse
            {
                Pillars = result.Adapt<ICollection<PillarResponse>>()
            };

            if (answerFetched is not null)
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

            return formResponse;
        }

        public async Task<FormResponse> CreateForm(FormRequest form)
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

            var answerFetched = await _contextConfig.Answers
                                    .FetchWithQuestionAndPillars()
                                    .FetchWithMonthRange()
                                    .FirstOrDefaultAsync();

            if (answerFetched is not null)
            {
                answerFetched.UpdatedAt = DateTime.Now;
                answerFetched.Pillars = request.Pillars!.Select(rp =>
                    {
                        var tempPillar = answerFetched.Pillars!.First(pp => pp.PillarId == rp.PillarId);

                        tempPillar.AdditionalData = rp.AdditionalData;

                        return tempPillar;
                    }).ToList();

                answerFetched.Questions = request.Questions!.Select(rq =>
                    {
                        var tempQuestion = answerFetched.Questions!.First(pp => pp.QuestionId == rq.QuestionId);

                        tempQuestion.Value = rq.Value;

                        return tempQuestion;
                    }).ToList();

                answerFetched?.Questions?.UpdateAllDates();
                answerFetched?.Pillars?.UpdateAllDates();

                _contextConfig.UpdateRange(answerFetched!);
            }
            else
            {
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
            }

            await _contextConfig.SaveChangesAsync();

            return new AnswerResponse();
        }
    }
}