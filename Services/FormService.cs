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

            if(answersRequestedIds is null) return new ErrorResponse(ErrorMessage.ProjectNotFound); 

                var anwsersFetched = await _contextConfig.Answers.Where(p 
                    => answersRequestedIds.Contains(p.QuestionId)).ToListAsync();

            anwsersFetched.ForEach(p => 
            {
                var anwser = request?.Questions?.FirstOrDefault(e => e.QuestionId == p.QuestionId);
                
                if (anwser is not null)
                    p.Value = anwser.Value == "DONE" ? true : false;
            });

            var newAnwsers = request.Questions?.Where(p
                => !answersRequestedIds.Contains(p.QuestionId))
                .Select(p => new Answer
                {
                    ProjectId = project.Id,
                    Value = p.Value == "DONE" ? true : false
                });

            _contextConfig.UpdateRange(anwsersFetched);

            if(newAnwsers is not null)
                await _contextConfig.AddRangeAsync(newAnwsers);
                
            await _contextConfig.SaveChangesAsync();

            return new AnswerResponse();
        }       
    }
}