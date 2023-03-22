using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Mapster;
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
    }
}