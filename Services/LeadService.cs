using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
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

        public async Task<IEnumerable<LeadResponse>> GetAllLeads()
        {
            var leads = await _contextConfig.Leads.ToListAsync();

            return leads.Adapt<List<LeadResponse>>();
        }
        public async Task<IActionResult> FindLead(Guid id)
        {
            var lead = await _contextConfig.Leads.FirstOrDefaultAsync(property => property.Id == id);

            if (lead is null) return new ErrorResponse("Lead Not Found");

            return lead.Adapt<LeadResponse>();
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

            await _contextConfig.AddAsync(leadEntity);
            await _contextConfig.SaveChangesAsync();

            var result = leadEntity.Adapt<LeadResponse>();

            result.SetStatusCode(201);

            return result;
        }
    }
}