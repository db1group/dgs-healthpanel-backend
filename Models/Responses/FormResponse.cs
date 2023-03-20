using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class FormResponse
    {
        public ICollection<PillarResponse>? Pillars { get; set; }
    }
}