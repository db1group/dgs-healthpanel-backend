namespace Db1HealthPanelBack.Entities
{
    public sealed class AnswerPillar
    {
        public Guid Id { get; set; }
        public Guid PillarId { get; set; }
        public Pillar? Pillar { get; set; }
        public string? AdditionalData { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}