using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Models.Responses.Errors;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<IEnumerable<LeadResponse>> GetAllLeads()
        {
            var leads = await _contextConfig.Leads.ToListAsync();

            return leads.Adapt<List<LeadResponse>>();
        }
        public async Task<IActionResult> FindLead(Guid id)
        {
            var lead = await _contextConfig.Leads
            .Include(property => property.LeadProjects)
            .FirstOrDefaultAsync(property => property.Id == id);


            if (lead is null) return new ErrorResponse("Lead Not Found");

            return lead.Adapt<LeadResponse>();
        }

        public async Task<IActionResult> UpdateLead(Guid id, LeadRequest lead)
        {
            var leadResult = await _contextConfig.Leads.Include(lead => lead.LeadProjects)
                                                        .FirstOrDefaultAsync(property => property.Id == id);

            if (leadResult is null) return new ErrorResponse("Lead Not Found");
            
            var isDuplicate = _contextConfig.Leads.Where(lead => lead.Name == leadResult.Name || lead.Email == leadResult.Email).Any();

            if (isDuplicate)
            {
                var resultError = leadResult.Adapt<LeadResponse>();

                resultError.SetStatusCode(409);

                return resultError;
            }

            leadResult.Name = lead.Name;
            leadResult.Email = lead.Email;
            leadResult.InTraining = lead.InTraining;

            if (lead.LeadProjects is not null)
            {
                leadResult.LeadProjects = leadResult.LeadProjects?
                                        .Where(ld => lead.LeadProjects!.Any(lds => lds.ProjectId == ld.ProjectId))
                                        .ToList();

                var projectsToAdd = lead.LeadProjects!
                                        .Where(ld => !leadResult.LeadProjects!.Any(lds => lds.ProjectId == ld.ProjectId))
                                                    .Adapt<List<LeadProject>>();
                                                    
                projectsToAdd.ForEach(pta => leadResult.LeadProjects!.Add(pta));
            } else leadResult.LeadProjects = new List<LeadProject>();

            _contextConfig.Leads.Update(leadResult);
            await _contextConfig.SaveChangesAsync();

            return leadResult.Adapt<LeadResponse>();
        }

        public async Task<IActionResult> DeleteLead(Guid id)
        {
            var lead = await _contextConfig.Leads.FirstOrDefaultAsync(property => property.Id == id);

            if (lead is null) return new ErrorResponse("Lead Not Found");

            _contextConfig.Remove(lead);
            await _contextConfig.SaveChangesAsync();

            return new ObjectResult(null) { StatusCode = 204 };
        }

        public async Task<IActionResult> CreateLead(LeadRequest lead)
        {
            var leadEntity = lead.Adapt<Lead>();

            var isDuplicate = _contextConfig.Leads.Where(lead => lead.Name == leadEntity.Name || lead.Email == leadEntity.Email).Any();

            if (isDuplicate)
            {
                var resultError = leadEntity.Adapt<LeadResponse>();

                resultError.SetStatusCode(409);

                return resultError;
            }

            await _contextConfig.AddAsync(leadEntity);
            await _contextConfig.SaveChangesAsync();

            var result = leadEntity.Adapt<LeadResponse>();

            result.SetStatusCode(201);

            return result;
        }
    }
}