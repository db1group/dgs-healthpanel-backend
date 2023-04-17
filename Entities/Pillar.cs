namespace Db1HealthPanelBack.Entities
{
    public sealed class Pillar
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? BackgroundColor { get; set; }
        public ICollection<Column>? Columns { get; set; }
        public int? Order { get; set; }
    }
}