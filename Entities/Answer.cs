namespace Db1HealthPanelBack.Entities
{
    public sealed class Answer
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public ICollection<AnswerQuestion>? Questions { get; set; }
        public ICollection<AnswerPillar>? Pillars { get; set; }
        public bool Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}