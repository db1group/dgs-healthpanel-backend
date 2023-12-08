using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GenerateKeyController : ControllerBase
    {
        private readonly GenerateKeyService _generateKeyService;

        public GenerateKeyController(GenerateKeyService generateKeyService)
            => _generateKeyService = generateKeyService;


        [HttpPost]
        public async Task<IActionResult> GenerateKey([FromBody] GenerateKeyRequest request)
            => await _generateKeyService.GenerateKey(request);
    }
}