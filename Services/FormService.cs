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

            return new FormResponse
            {
                Pillars = result.Adapt<ICollection<PillarResponse>>()
            };
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
                => p.Name!.ToLower() == request.Project!.ToLower());

            if(project is null) return new ErrorResponse(ErrorMessage.ProjectNotFound);
            
            var answersRequestedIds = request.Questions?.Select(q => q.QuestionId);

            var actualMonth = DateTime.Now.ToString("MM");
            var lastMonth = DateTime.Now.AddMonths(-1).ToString("MM");

            if(answersRequestedIds is null) return new ErrorResponse(ErrorMessage.QuestionNeeded); 

            var answersFetched = await _contextConfig.AnswersQuestions.Where(p 
                => !answersRequestedIds.Any(an => p.QuestionId == an) 
                    && actualMonth == p.CreatedAt.ToString("MM") || lastMonth == p.CreatedAt.ToString("MM")).ToListAsync();

            answersFetched.ForEach(p => 
            {
                var answer = request?.Questions?.FirstOrDefault(e => e.QuestionId == p.QuestionId);
                
                // if (answer is not null)
                //     p.Value = answer.Value == "DONE" ? true : false;
            });

            var newAnswers = request.Questions?.Where(p
                => !answersRequestedIds.Contains(p.QuestionId))
                .Select(p => new Answer
                {
                    ProjectId = project.Id,
                    Value = p.Value == "DONE" ? true : false
                });

            _contextConfig.UpdateRange(answersFetched);

            if(newAnswers is not null)
                await _contextConfig.AddRangeAsync(newAnswers);
                
            await _contextConfig.SaveChangesAsync();

            return new AnswerResponse();
        }       
    }
}