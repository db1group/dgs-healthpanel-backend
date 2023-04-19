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
    public class LeadService
    {
        private readonly ContextConfig _contextConfig;

        public LeadService(ContextConfig contextConfig)
        {
            _contextConfig = contextConfig;
        }


        public async Task<LeadResponse> CreateLeadEngineer(LeadRequest leadRequest)
        {
            var lead = leadRequest.Adapt<Lead>();
            var projectName = _contextConfig.Projects.FirstOrDefault(p => p.Id == leadRequest.ProjectId);

            if (lead is not null && projectName is not null)
            {
                await _contextConfig.Leads.AddAsync(lead);
                await _contextConfig.SaveChangesAsync();
            }

            return new LeadResponse { Name = lead.Name, ProjectName = projectName.Name };


        }


    }
}