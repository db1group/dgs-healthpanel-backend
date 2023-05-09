using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Models.Responses;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Services
{
    public class CostCenterService
    {
        private readonly ContextConfig _contextConfig;

        public CostCenterService(ContextConfig contextConfig)
        {
            _contextConfig = contextConfig;
        }

        public async Task<IEnumerable<CostCenterResponse>> GetAll()
            => (await _contextConfig.CostCenters.ToListAsync()).Adapt<IEnumerable<CostCenterResponse>>();
    }
}