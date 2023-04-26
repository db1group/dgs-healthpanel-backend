namespace Db1HealthPanelBack.Models.Responses
{
    public class PillarResponse
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? BackgroundColor { get; set; }
        public ICollection<ColumnResponse>? Columns { get; set; }
        public string? AdditionalData { get; set; }
    }
}