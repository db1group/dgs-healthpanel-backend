using Db1HealthPanelBack.Configs;
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
            var result = await _contextConfig.Pillars.ToListAsync();

            return new FormResponse
            {
                Pillars = result.Adapt<ICollection<PillarResponse>>()
            };
        }
    }
}