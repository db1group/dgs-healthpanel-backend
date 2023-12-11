using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
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

        /// <summary>
        /// Generate a key for the lead request
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /generatekey
        ///     {
        ///         "projectId": "Project Id",
        ///         "leadId": "Lead Id",
        ///     }
        ///     
        /// </remarks>
        /// <returns>A key and time it was generated</returns>
        [HttpPost]
        [ProducesResponseType<GenerateKeyResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateKey([FromBody] GenerateKeyRequest request)
            => await _generateKeyService.GenerateKey(request);
    }
}
