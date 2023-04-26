using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Mapster;

namespace Db1HealthPanelBack.Services
{
    public class LeadService
    {
        private readonly ContextConfig _contextConfig;

        public LeadService(ContextConfig contextConfig)
        {
            _contextConfig = contextConfig;
        }


        public async Task<LeadResponse> CreateLeadEngineer(LeadRequest leadRequest)
        {
            Console.WriteLine(leadRequest);
            var lead = leadRequest.Adapt<Lead>();

            var projectName = _contextConfig.Projects.FirstOrDefault(p => p.Id == leadRequest.ProjectId);

            if (lead is not null && projectName is not null)
            {

                var leadProject = new LeadProject
                {
                    Lead = lead,
                    ProjectId = leadRequest.ProjectId
                };

                await _contextConfig.LeadProject.AddAsync(leadProject);
                await _contextConfig.SaveChangesAsync();

                return new LeadResponse(lead.Name, projectName.Name, lead.InTraining)
                    .Adapt<LeadResponse>();
            }

            throw new Exception("Lead or Project not found");
        }


    }
}