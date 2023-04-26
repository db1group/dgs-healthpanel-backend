namespace Db1HealthPanelBack.Entities
{
    public sealed class Column
    {
        public Guid Id { get; set; }
        public Guid PillarId { get; set; }
        public string? Title { get; set; }
        public decimal? Weight { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public int? Order { get; set; }
        public string? AdditionalData { get; set; }
    }
}