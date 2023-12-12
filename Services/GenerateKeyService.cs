using Db1HealthPanelBack.Configs;
using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Models.Responses;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Services
{
    public class GenerateKeyService
    {
        private readonly ContextConfig _contextConfig;

        public GenerateKeyService(ContextConfig contextConfig)
        {
            _contextConfig = contextConfig;
        }

        public async Task<IActionResult> GenerateKey(GenerateKeyRequest request)
        {
            var newKey = new KeyDB1CLI
            {
                Key = Guid.NewGuid(),
                ProjectId = request.ProjectId,
                LeadId = request.LeadId
            };

            await _contextConfig.AddAsync(newKey);
            await _contextConfig.SaveChangesAsync();

            return newKey.Adapt<GenerateKeyResponse>();
        }
    }
}