using Microsoft.AspNetCore.Mvc;

namespace Db1HealthPanelBack.Models.Responses
{
    public class PillarResponse
    {
        public string? Title { get; set; }
        public string? BackgroundColor { get; set; }
        public ICollection<ColumnResponse>? Columns { get; set; }
    }
}