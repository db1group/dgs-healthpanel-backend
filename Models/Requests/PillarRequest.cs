namespace Db1HealthPanelBack.Models.Requests
{
    public class PillarRequest
    {
        public string? Title { get; set; }
        public string? BackgroundColor { get; set; }
        public ICollection<ColumnRequest>? Columns { get; set; }
    }
}