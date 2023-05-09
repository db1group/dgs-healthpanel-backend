using Db1HealthPanelBack.Models.Responses;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CostCenterController : ControllerBase
    {
        private readonly CostCenterService _costCenterService;

        public CostCenterController(CostCenterService costCenterService)
        {
            _costCenterService = costCenterService;
        }

        public async Task<IEnumerable<CostCenterResponse>> GetAll()
            => await _costCenterService.GetAll();
    }
}